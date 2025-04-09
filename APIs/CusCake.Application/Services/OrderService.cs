using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using CusCake.Application.Extensions;
using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.Services.IServices;
using CusCake.Application.Utils;
using CusCake.Application.ViewModels.OrderModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using UnauthorizedAccessException = CusCake.Application.GlobalExceptionHandling.Exceptions.UnauthorizedAccessException;

namespace CusCake.Application.Services;

public interface IOrderService
{
    Task<Order> CreateAsync(OrderCreateModel model);
    Task<Order> GetOrderByIdAsync(Guid id);
    Task<Order> UpdateAsync(Guid id, OrderUpdateModel model);
    Task<Order> CancelAsync(Guid id);
    Task BakeryConfirmAsync(Guid id);
    Task<Order?> MoveToNextAsync<Order>(Guid id, List<IFormFile>? files = null);
    Task<(Pagination, List<Order>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<Order, bool>>? filter = null);
    Task<Order> GetOrderDetailAsync(Guid id);
}

public class OrderService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ICustomerService customerService,
    IClaimsService claimsService,
    IGoongService goongService,
    IVoucherService voucherService,
    INotificationService notificationService,
    IFileService fileService,
    IAuthService authService,
    IWalletService walletService,
    IBackgroundJobClient backgroundJobClient
) : IOrderService
{
    private readonly IVoucherService _voucherService = voucherService;
    private readonly IGoongService _goongService = goongService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IClaimsService _claimsService = claimsService;
    private readonly ICustomerService _customerService = customerService;
    private readonly INotificationService _notificationService = notificationService;
    private readonly IFileService _fileService = fileService;
    private readonly IAuthService _authService = authService;
    private readonly IWalletService _walletService = walletService;
    private readonly IBackgroundJobClient _backgroundJobClient = backgroundJobClient;

    public Dictionary<string, object> GetTransitions()
    {
        return new Dictionary<string, object>
        {
            {
                "PENDING", new OrderStateTransition<Order>
                {
                    From = [OrderStatusConstants.PENDING],
                    To = OrderStatusConstants.WAITING_BAKERY,
                    Guard = (order) =>
                    {
                        if(order.ShippingType==ShippingTypeConstants.DELIVERY && order.Transaction!=null)
                             return true;

                        if(order!.CustomerId != _claimsService.GetCurrentUser ||
                            _claimsService.GetCurrentUserRole != RoleConstants.CUSTOMER )
                            throw new UnauthorizedAccessException("Can not access to action!");

                        return true;
                    },
                    Action = async (order) =>
                    {

                        order!.OrderStatus = OrderStatusConstants.WAITING_BAKERY;
                        order!.PaidAt=DateTime.Now;
                        _unitOfWork.OrderRepository.Update(order!);
                        await _unitOfWork.SaveChangesAsync();

                        var localExecuteTime = DateTime.Now.AddMinutes(5);
                        var delay = localExecuteTime - DateTime.Now;

                        _backgroundJobClient.Schedule(() => BakeryConfirmAsync(order!.Id), delay);

                        return order!;
                    }
                }
            },
            {
                "WAITING_BAKERY_CONFIRM", new OrderStateTransition<Order>
                {
                    From = [OrderStatusConstants.WAITING_BAKERY],
                    To = OrderStatusConstants.PROCESSING,
                    Guard = (order) =>
                    {
                        if(_claimsService.GetCurrentUserRole!= RoleConstants.BAKERY || order.BakeryId != _claimsService.GetCurrentUser)
                            throw new UnauthorizedAccessException("Can not access to action!");
                        return true;
                    },
                    Action=async (order)=> {

                        order!.OrderStatus = OrderStatusConstants.PROCESSING;
                        _unitOfWork.OrderRepository.Update(order!);

                        await _unitOfWork.SaveChangesAsync();
                        var notification = new Notification
                        {
                            Title = NotificationType.GetTitleByType(NotificationType.PROCESSING_ORDER),
                            Content = NotificationType.GetContentByType(NotificationType.PROCESSING_ORDER),
                            Type = NotificationType.PROCESSING_ORDER,
                            TargetEntityId = order.Id
                        };

                        var orderJson = JsonConvert.SerializeObject(order);
                        await _notificationService.CreateOrderNotificationAsync(order.Id,NotificationType.PROCESSING_ORDER, null ,order.CustomerId);
                        await _notificationService.SendNotificationAsync(order.CustomerId, orderJson, NotificationType.PROCESSING_ORDER);

                        return order!;

                    }
                }
            },
            {
                "PROCESSING", new OrderStateTransition<Order>
                {
                    From = [OrderStatusConstants.PROCESSING],
                    To = OrderStatusConstants.SHIPPING,
                    Guard = (order) =>
                    {
                         if(_claimsService.GetCurrentUserRole!= RoleConstants.BAKERY || order.BakeryId != _claimsService.GetCurrentUser)
                            throw new UnauthorizedAccessException("Can not access to action!");
                        return true;
                    },
                    Validate=async (param)=>{
                        var (order, files) = param;

                        if(order.ShippingType == ShippingTypeConstants.PICK_UP) return true;

                        if (files==null || files.Count == 0)
                             throw new BadRequestException("Should provide image or video about order!");

                        var ordersSupport= new List<OrderSupport>();

                        foreach (var file in files)
                        {
                            var upload= await _fileService.UploadFileAsync(file);
                            ordersSupport.Add(new OrderSupport{
                                FileId=upload.Id,
                                CustomerId=order.CustomerId,
                                BakeryId=order.BakeryId,
                                OrderId=order.Id
                            });
                        }
                        await _unitOfWork.OrderSupportRepository.AddRangeAsync(ordersSupport);
                        await _unitOfWork.SaveChangesAsync();

                        return true;
                    },
                    Action=async (order) =>
                    {
                        var status=  order.ShippingType == ShippingTypeConstants.PICK_UP ?
                                OrderStatusConstants.READY_FOR_PICKUP :
                                OrderStatusConstants.SHIPPING;
                        order!.OrderStatus = status;

                        _unitOfWork.OrderRepository.Update(order!);
                        await _unitOfWork.SaveChangesAsync();

                        var orderJson = JsonConvert.SerializeObject(order);
                        await _notificationService.CreateOrderNotificationAsync(order.Id, status , null ,order.CustomerId);
                        await _notificationService.SendNotificationAsync(order.CustomerId, orderJson, status);

                        var completed_time=  order.ShippingType == ShippingTypeConstants.PICK_UP ? 30 :order.ShippingTime!.Value;
                        var localExecuteTime = DateTime.Now.AddMinutes(completed_time + 15);
                        var delay = localExecuteTime - DateTime.Now;


                        if( order.ShippingType == ShippingTypeConstants.PICK_UP)
                            _backgroundJobClient.Schedule(() => AutoCancelAsync(order!.Id), delay);
                        else
                            _backgroundJobClient.Schedule(() => AutoShippingCompletedAsync(order!.Id), delay);

                        return order!;
                    }
                }
            },
            {
                "SHIPPING", new OrderStateTransition<Order>
                {
                    From = [OrderStatusConstants.SHIPPING],
                    To = OrderStatusConstants.COMPLETED,
                    Action=async (order) =>
                    {
                        order!.OrderStatus = OrderStatusConstants.COMPLETED;
                        _unitOfWork.OrderRepository.Update(order!);
                        await _unitOfWork.SaveChangesAsync();

                         await MakeFinalPayment(order);

                        var orderJson = JsonConvert.SerializeObject(order);
                        await _notificationService.CreateOrderNotificationAsync(order.Id,NotificationType.COMPLETED_ORDER, null ,order.CustomerId);
                        await _notificationService.SendNotificationAsync(order.CustomerId, orderJson, NotificationType.COMPLETED_ORDER);

                        return order!;
                    }
                }
            },
            {
                "READY_FOR_PICKUP", new OrderStateTransition<Order>
                {
                    From = [OrderStatusConstants.READY_FOR_PICKUP],
                    To = OrderStatusConstants.COMPLETED,
                    Action=async (order) =>
                    {
                        order!.OrderStatus = OrderStatusConstants.COMPLETED;
                        _unitOfWork.OrderRepository.Update(order!);
                        await _unitOfWork.SaveChangesAsync();

                        await MakeFinalPayment(order);

                        var orderJson = JsonConvert.SerializeObject(order);
                        await _notificationService.CreateOrderNotificationAsync(order.Id,NotificationType.COMPLETED_ORDER, null ,order.CustomerId);
                        await _notificationService.SendNotificationAsync(order.CustomerId, orderJson, NotificationType.COMPLETED_ORDER);

                        return order!;
                    }
                }
            }
        };
    }

    private async Task MakeFinalPayment(Order order)
    {
        var adminWallet = (await _authService.GetAdminAsync()).Wallet;
        await _walletService.MakeBillingAsync(adminWallet, -order.ShopRevenue, WalletTransactionTypeConstants.ADMIN_TO_BAKERY_TRANSFER);

        var bakeryWallet = (await _authService.GetAuthByIdAsync(order.BakeryId)).Wallet;
        await _walletService.MakeBillingAsync(bakeryWallet, order.ShopRevenue, WalletTransactionTypeConstants.SHOP_REVENUE_TRANSFER);

    }

    public async Task BakeryConfirmAsync(Guid id)
    {
        var order = await GetOrderByIdAsync(id);
        if (order.OrderStatus != OrderStatusConstants.WAITING_BAKERY) return;

        await MoveToNextAsync<Order>(id);
    }

    public async Task AutoShippingCompletedAsync(Guid id)
    {
        var order = await GetOrderByIdAsync(id);
        if (order.OrderStatus != OrderStatusConstants.SHIPPING) return;

        await MoveToNextAsync<Order>(id);
    }

    public async Task AutoCancelAsync(Guid id)
    {
        var order = await GetOrderByIdAsync(id);
        if (order.OrderStatus == OrderStatusConstants.COMPLETED) return;

        order!.OrderStatus = OrderStatusConstants.CANCELED;
        _unitOfWork.OrderRepository.Update(order!);
        await _unitOfWork.SaveChangesAsync();

        var orderJson = JsonConvert.SerializeObject(order);
        await _notificationService.CreateOrderNotificationAsync(order.Id, NotificationType.CANCELED_ORDER, order.BakeryId, null);
        await _notificationService.SendNotificationAsync(order.BakeryId, orderJson, NotificationType.CANCELED_ORDER);
    }

    public async Task<Order> CreateAsync(OrderCreateModel model)
    {

        var bakery = await _unitOfWork
            .BakeryRepository
            .FirstOrDefaultAsync(x =>
                x.Id == model.BakeryId &&
                x.Status == BakeryStatusConstants.CONFIRMED
            ) ?? throw new BadRequestException("Bakery not found");

        var customer = await _customerService.GetByIdAsync(_claimsService.GetCurrentUser);

        if (string.IsNullOrEmpty(model.ShippingAddress) && string.IsNullOrEmpty(customer.Address))
            throw new BadRequestException("Shipping Address is required");

        model.PhoneNumber ??= customer.Phone;
        model.ShippingAddress ??= customer.Address;
        model.Longitude ??= customer.Longitude;
        model.Latitude ??= customer.Latitude;

        var order = _mapper.Map<Order>(model);
        order.CustomerId = _claimsService.GetCurrentUser;
        var getOrderDetail = await GetOrderDetails(order, model.OrderDetailCreateModels);
        order.TotalProductPrice = getOrderDetail.Item2;

        order = await CalculateShipping(bakery, order);

        order = await CalculateDiscount(order);

        order = CalculateFinalPrice(order);

        order.OrderCode = GenerateRandomString.GenerateCode();

        await _unitOfWork.OrderRepository.AddAsync(order);
        await _unitOfWork.SaveChangesAsync();

        return order;
    }


    private async Task<Order> CalculateDiscount(Order order)
    {
        if (string.IsNullOrEmpty(order.VoucherCode))
        {
            order.VoucherCode = null;
            order.VoucherId = null;
            order.DiscountAmount = 0;
            return order;
        }

        CustomerVoucher cus_voucher = null!;

        var voucher = await _voucherService.GetVoucherByCodeAsync(order.VoucherCode!, order.BakeryId)
            ?? throw new BadRequestException("Voucher code is invalid or does not exist.");

        if (voucher.VoucherType == VoucherTypeConstants.PRIVATE || voucher.VoucherType == VoucherTypeConstants.SYSTEM)
            cus_voucher = await _unitOfWork.CustomerVoucherRepository
                .FirstOrDefaultAsync(x =>
                    x.CustomerId == _claimsService.GetCurrentUser &&
                    x.VoucherId == voucher.Id &&
                    x.IsApplied == false
                ) ?? throw new BadRequestException("Voucher code is invalid or does not exist.");

        if (voucher.UsageCount >= voucher.Quantity)
            throw new BadRequestException("This voucher has been used up and is no longer available.");

        if (voucher.ExpirationDate < DateTime.Now)
            throw new BadRequestException("This voucher has expired and can no longer be used.");

        if (voucher.MinOrderAmount > order.TotalProductPrice)
            throw new BadRequestException($"The total order amount must be greater than or equal to {voucher.MinOrderAmount:C} to use this voucher.");

        var discountAmount = order.TotalProductPrice * voucher.DiscountPercentage / 100;

        order.DiscountAmount = Math.Min(discountAmount, voucher.MaxDiscountAmount);

        order.VoucherId = voucher.Id;
        order.CustomerVoucherId = cus_voucher?.Id;
        UpdateVoucherCount(order.Id, voucher, cus_voucher!);
        return order;
    }


    private void UpdateVoucherCount(Guid orderId, Voucher voucher, CustomerVoucher cus_voucher)
    {
        if (voucher.VoucherType == VoucherTypeConstants.GLOBAL)
        {
            voucher.UsageCount += 1;
            _unitOfWork.VoucherRepository.Update(voucher);
            return;
        }
        cus_voucher.OrderId = orderId;
        cus_voucher.IsApplied = true;
        cus_voucher.AppliedAt = DateTime.Now;
        _unitOfWork.CustomerVoucherRepository.Update(cus_voucher);

    }

    private async Task<Order> CalculateShipping(Bakery bakery, Order order)
    {
        if (order.ShippingType == ShippingTypeConstants.PICK_UP || order.PaymentType == PaymentTypeConstants.CASH)
        {
            order.ShippingAddress = null;
            order.Longitude = null;
            order.Latitude = null;
            order.ShippingTime = null;
            order.ShippingFee = 0;
            order.ShippingType = ShippingTypeConstants.PICK_UP;
            return order;
        }


        var (distanceKm, shippingTimeH) = await _goongService.GetShippingInfoAsync(
            bakery.Latitude, bakery.Longitude,
            order.Latitude!, order.Longitude!
        );

        if (distanceKm == 0) throw new BadRequestException("Invalid address!");

        double shipping_fee = DeliveryFeeCalculator.CalculateFee(distanceKm);

        order.ShippingDistance = distanceKm;
        order.ShippingFee = shipping_fee;
        order.ShippingTime = shippingTimeH;
        order.ShippingType = ShippingTypeConstants.DELIVERY;

        return order;
    }

    private static Order CalculateFinalPrice(Order order)
    {
        double totalCustomerPaid = order.TotalProductPrice + order.ShippingFee - order.DiscountAmount;
        double appCommissionFee = totalCustomerPaid * order.CommissionRate;
        double shopRevenue = totalCustomerPaid - appCommissionFee;

        order.AppCommissionFee = appCommissionFee;
        order.ShopRevenue = shopRevenue;
        order.TotalCustomerPaid = totalCustomerPaid;

        return order;
    }

    private async Task<(List<OrderDetail>, double)> GetOrderDetails(Order order, List<OrderDetailCreateModel> orderDetails)
    {
        var details = _mapper.Map<List<OrderDetail>>(orderDetails);
        double total = 0;
        for (int i = 0; i < orderDetails.Count; i++)
        {
            var available_cake_id = orderDetails[i].AvailableCakeId;
            var customer_cake_id = orderDetails[i].CustomCakeId;
            details[i].OrderId = order.Id;

            if (available_cake_id.HasValue)
            {
                var availableCake = await _unitOfWork.AvailableCakeRepository.FirstOrDefaultAsync(x =>
                        x.BakeryId == order.BakeryId &&
                        x.Id == available_cake_id.Value); ;
                total += (double)(availableCake!.AvailableCakePrice * details[i].Quantity)!;
                details[i].SubTotalPrice = availableCake.AvailableCakePrice;
            }
            if (customer_cake_id.HasValue)
            {
                var customCake = await _unitOfWork.CustomCakeRepository.FirstOrDefaultAsync(x =>
                        x.BakeryId == order.BakeryId &&
                        x.Id == customer_cake_id.Value);
                total += (double)(customCake!.Price * details[i].Quantity)!;
                details[i].SubTotalPrice = customCake.Price;
            }
        }
        if (details.Count != orderDetails.Count)
            throw new BadRequestException("Only buy cake in one store at time!");

        await _unitOfWork.OrderDetailRepository.AddRangeAsync(details);

        return (details, total);
    }

    public async Task<Order> GetOrderDetailAsync(Guid id)
    {
        var includes = QueryHelper.Includes<Order>(
            x => x.Customer!,
            x => x.Bakery!,
            x => x.Voucher!,
            x => x.CustomerVoucher!);

        var order = await _unitOfWork.OrderRepository.GetByIdAsync(id, includes: includes)
            ?? throw new BadRequestException("Order not found!");
        order.Transaction = await _unitOfWork.TransactionRepository.FirstOrDefaultAsync(x => x.OrderId == order.Id);

        order.OrderDetails = await _unitOfWork.OrderDetailRepository.WhereAsync(x => x.OrderId == id, includes: x => x.CakeReview!);

        order.OrderSupports = await _unitOfWork.OrderSupportRepository
                            .WhereAsync(x =>
                                x.BakeryId == order.BakeryId &&
                                x.OrderId == order.Id &&
                                x.CustomerId == order.CustomerId
                            );
        return order;
    }
    public async Task<Order> GetOrderByIdAsync(Guid id)
    {
        var includes = QueryHelper.Includes<Order>(
            x => x.Voucher!,
            x => x.CustomerVoucher!);

        var order = await _unitOfWork.OrderRepository.GetByIdAsync(id, includes: includes)
            ?? throw new BadRequestException("Order not found!");
        order.Transaction = await _unitOfWork.TransactionRepository.FirstOrDefaultAsync(x => x.OrderId == order.Id);

        return order;
    }

    private async Task DeleteOrderDetailAsync(Order order)
    {
        var order_details = await _unitOfWork.OrderDetailRepository.WhereAsync(x => x.OrderId == order.Id);
        _unitOfWork.OrderDetailRepository.SoftRemoveRange(order_details!);
    }

    private async Task ResetVoucherAsync(string voucherCode, Guid bakeryId)
    {
        var voucher = await _voucherService.GetVoucherByCodeAsync(voucherCode, bakeryId)
         ?? throw new BadRequestException("Voucher code is invalid or does not exist.");

        if (voucher.VoucherType == VoucherTypeConstants.GLOBAL)
        {
            voucher.UsageCount += 1;
            _unitOfWork.VoucherRepository.Update(voucher);
            await _unitOfWork.SaveChangesAsync();
            return;
        }

        var cus_voucher = await _unitOfWork.CustomerVoucherRepository
                .FirstOrDefaultAsync(x =>
                    x.CustomerId == _claimsService.GetCurrentUser &&
                    x.VoucherId == voucher.Id &&
                    x.IsApplied == false
                ) ?? throw new BadRequestException("Voucher code is invalid or does not exist.");

        cus_voucher.IsApplied = false;
        cus_voucher.AppliedAt = null;
        _unitOfWork.CustomerVoucherRepository.Update(cus_voucher);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Order> UpdateAsync(Guid id, OrderUpdateModel model)
    {
        var bakery = await _unitOfWork
           .BakeryRepository
           .FirstOrDefaultAsync(x =>
               x.Id == model.BakeryId &&
               x.Status == BakeryStatusConstants.CONFIRMED
           ) ?? throw new BadRequestException("Bakery not found");

        var customer = await _customerService.GetByIdAsync(_claimsService.GetCurrentUser);

        model.PhoneNumber ??= customer.Phone;
        model.ShippingAddress ??= customer.Address;
        model.Longitude ??= customer.Longitude;
        model.Latitude ??= customer.Latitude;

        if (string.IsNullOrEmpty(model.ShippingAddress) && string.IsNullOrEmpty(customer.Address))
            throw new BadRequestException("Shipping Address is required");

        var order = await GetOrderByIdAsync(id);
        var old_voucherCode = order.VoucherCode;
        var old_ShippingAddress = order.VoucherCode;
        var old_ShippingType = order.ShippingType;

        if (!new[] {
            OrderStatusConstants.PENDING,
            // OrderStatusConstants.CONFIRMED,
            // OrderStatusConstants.PAYMENT_PENDING
        }.Contains(order.OrderStatus))
            throw new BadRequestException("Only update before PAID!");

        await DeleteOrderDetailAsync(order);

        _mapper.Map(model, order);

        var getOrderDetail = await GetOrderDetails(order, model.OrderDetailCreateModels);
        order.TotalProductPrice = getOrderDetail.Item2;

        if (model.ShippingType != old_ShippingType || model.ShippingAddress != old_ShippingAddress)
            order = await CalculateShipping(bakery, order);

        if (model.VoucherCode != old_voucherCode)
            order = await CalculateDiscount(order);

        order = CalculateFinalPrice(order);
        order.OrderStatus = OrderStatusConstants.PENDING;

        _unitOfWork.OrderRepository.Update(order);
        await _unitOfWork.SaveChangesAsync();

        if (model.VoucherCode != old_voucherCode && old_voucherCode != null)
            await ResetVoucherAsync(old_voucherCode, order.BakeryId);

        return order;

    }


    public async Task<Order> CancelAsync(Guid id)
    {
        var order = await GetOrderByIdAsync(id);

        if (new[] {
            OrderStatusConstants.PROCESSING,
            OrderStatusConstants.READY_FOR_PICKUP,
            OrderStatusConstants.SHIPPING,
            OrderStatusConstants.COMPLETED,
            OrderStatusConstants.CANCELED,
        }.Contains(order.OrderStatus))
            throw new BadRequestException("Only cancel before bakery CONFIRMED!");

        order.OrderStatus = OrderStatusConstants.CANCELED;
        order.CancelBy = _claimsService.GetCurrentUserRole;

        _unitOfWork.OrderRepository.Update(order);
        await _unitOfWork.SaveChangesAsync();

        if (new[] {
            OrderStatusConstants.WAITING_BAKERY,
        }.Contains(order.OrderStatus))
            await RollbackMoneyAsync(order);

        if (!string.IsNullOrEmpty(order.VoucherCode))
            await ResetVoucherAsync(order.VoucherCode!, order.BakeryId);


        var orderJson = JsonConvert.SerializeObject(order);
        await _notificationService.CreateOrderNotificationAsync(order.Id, NotificationType.COMPLETED_ORDER, null, order.CustomerId);
        await _notificationService.SendNotificationAsync(order.CustomerId, orderJson, NotificationType.COMPLETED_ORDER);

        return order;
    }

    private async Task RollbackMoneyAsync(Order order)
    {
        var adminWallet = (await _authService.GetAdminAsync()).Wallet;
        await _walletService.MakeBillingAsync(adminWallet, -order.TotalCustomerPaid, WalletTransactionTypeConstants.ROLL_BACK);

        var customerWallet = (await _authService.GetAuthByIdAsync(order.CustomerId)).Wallet;
        await _walletService.MakeBillingAsync(customerWallet, order.TotalCustomerPaid, WalletTransactionTypeConstants.ROLL_BACK);

    }

    public async Task<Order?> MoveToNextAsync<Order>(Guid id, List<IFormFile>? files = null)
    {
        var order = await GetOrderByIdAsync(id);

        var transitions = GetTransitions();

        if (!transitions.TryGetValue(order.OrderStatus!, out var transitionObj))
            throw new BadRequestException("Invalid state transition!");

        var transition = transitionObj as OrderStateTransition<Order> ?? throw new BadRequestException("Invalid state transition!");

        transition.Guard?.Invoke(order);

        // transition.Validate?.Invoke((order, files)!);
        if (transition.Validate != null)
        {
            await transition.Validate((order, files!));
        }

        return transition.Action != null ? await transition.Action(order) : default;
    }

    public async Task<(Pagination, List<Order>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<Order, bool>>? filter = null)
    {
        var includes = QueryHelper.Includes<Order>(
           x => x.Customer!,
           x => x.Bakery!,
           x => x.Voucher!,
           x => x.CustomerVoucher!);

        return await _unitOfWork.OrderRepository.ToPagination(pageIndex, pageSize, filter: filter, includes: includes);

    }
}
