using System.Linq.Expressions;
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
                    To = OrderStatusConstants.CONFIRMED,
                    Guard = (order) =>
                    {
                        if(order!.CustomerId != _claimsService.GetCurrentUser ||
                            _claimsService.GetCurrentUserRole != RoleConstants.CUSTOMER )
                            throw new UnauthorizedAccessException("Can not access to action!");

                        return true;
                    },
                    Action = async (order) =>
                    {
                        order!.OrderStatus = OrderStatusConstants.CONFIRMED;
                        _unitOfWork.OrderRepository.Update(order!);
                        await _unitOfWork.SaveChangesAsync();
                        return order!;
                    }
                }
            },
            {
                "CONFIRMED", new OrderStateTransition<Order>
                {
                    From = [OrderStatusConstants.CONFIRMED],
                    To = OrderStatusConstants.PAYMENT_PENDING,
                     Guard = (order) =>
                    {
                        if(order!.CustomerId != _claimsService.GetCurrentUser ||
                            _claimsService.GetCurrentUserRole != RoleConstants.CUSTOMER )
                            throw new UnauthorizedAccessException("Can not access to action!");


                        return true;
                    },
                    Action = async (order) =>
                    {
                        var status=  order.ShippingType == ShippingTypeConstants.PICK_UP?
                            OrderStatusConstants.WAITING_BAKERY :
                            OrderStatusConstants.PAYMENT_PENDING;

                        order!.OrderStatus = status;
                        _unitOfWork.OrderRepository.Update(order!);
                        await _unitOfWork.SaveChangesAsync();

                        if(status == OrderStatusConstants.WAITING_BAKERY){
                            var localExecuteTime = DateTime.Now.AddHours(7).AddMinutes(5);
                            var delay = localExecuteTime - DateTime.UtcNow;
                            _backgroundJobClient.Schedule(() => BakeryConfirmAsync(order!.Id), delay);
                        }

                        return order!;
                    }
                }
            },
            {
                "PAYMENT_PENDING", new OrderStateTransition<Order>
                {
                    From = [OrderStatusConstants.PAYMENT_PENDING],
                    To = OrderStatusConstants.PAID,
                    Guard = (order) =>
                    {
                        return true;
                    },
                    Action = async (order) =>
                    {
                        order!.OrderStatus = OrderStatusConstants.PAID;
                        order!.PaidAt=DateTime.Now;
                        _unitOfWork.OrderRepository.Update(order!);
                        await _unitOfWork.SaveChangesAsync();
                        return order!;
                    }
                }
            },
            {
                "PAID", new OrderStateTransition<Order>
                {
                    From = [OrderStatusConstants.PAID],
                    To = OrderStatusConstants.WAITING_BAKERY,
                    Action = async (order) =>
                    {
                        order!.OrderStatus = OrderStatusConstants.WAITING_BAKERY;
                        _unitOfWork.OrderRepository.Update(order!);
                        await _unitOfWork.SaveChangesAsync();
                        return order!;
                    }
                }
            },
            {
                "WAITING_BAKERY", new OrderStateTransition<Order>
                {
                    From = [OrderStatusConstants.WAITING_BAKERY],
                    To = OrderStatusConstants.PROCESSING,
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

                        var localExecuteTime = DateTime.Now.AddHours(7).AddMinutes(completed_time);
                        var delay = localExecuteTime - DateTime.UtcNow;
                        _backgroundJobClient.Schedule(() => AutoCompletedAsync(order!.Id), delay);


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
                        await _notificationService.CreateOrderNotificationAsync(order.Id,NotificationType.ORDER_COMPLETED, null ,order.CustomerId);
                        await _notificationService.SendNotificationAsync(order.CustomerId, orderJson, NotificationType.ORDER_COMPLETED);

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
                        await _notificationService.CreateOrderNotificationAsync(order.Id,NotificationType.ORDER_COMPLETED, null ,order.CustomerId);
                        await _notificationService.SendNotificationAsync(order.CustomerId, orderJson, NotificationType.ORDER_COMPLETED);

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

    public async Task AutoCompletedAsync(Guid id)
    {
        var order = await GetOrderByIdAsync(id);
        if (order.OrderStatus != OrderStatusConstants.SHIPPING) return;

        await MoveToNextAsync<Order>(id);
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
        var getOrderDetail = await GetOrderDetails(order.Id, model.OrderDetailCreateModels);
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
            order.DiscountAmount = 0;
            order.VoucherId = null;
            return order;
        }

        var voucher = await _voucherService.GetVoucherByCodeAsync(order.VoucherCode!, order.BakeryId)
                        ?? throw new BadRequestException("Invalid voucher code!");

        var discountAmount = order.TotalProductPrice * voucher.DiscountPercentage;
        order.DiscountAmount = discountAmount;
        order.VoucherId = voucher.Id;

        return order;
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

    private async Task<(List<OrderDetail>, double)> GetOrderDetails(Guid orderId, List<OrderDetailCreateModel> orderDetails)
    {
        var details = _mapper.Map<List<OrderDetail>>(orderDetails);
        double total = 0;
        for (int i = 0; i < orderDetails.Count; i++)
        {
            var available_cake_id = orderDetails[i].AvailableCakeId;
            var customer_cake_id = orderDetails[i].CustomCakeId;
            details[i].OrderId = orderId;

            if (available_cake_id.HasValue)
            {
                var availableCake = await _unitOfWork.AvailableCakeRepository.GetByIdAsync(available_cake_id.Value); ;
                total += availableCake!.AvailableCakePrice;
                details[i].SubTotalPrice = availableCake.AvailableCakePrice;
            }
            if (customer_cake_id.HasValue)
            {
                var customCake = await _unitOfWork.CustomCakeRepository.GetByIdAsync(customer_cake_id.Value);
                total += customCake!.Price;
                details[i].SubTotalPrice = customCake.Price;
            }
        }

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

        order.OrderDetails = await _unitOfWork.OrderDetailRepository.WhereAsync(x => x.OrderId == id);
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

        return await _unitOfWork.OrderRepository.GetByIdAsync(id, includes: includes)
            ?? throw new BadRequestException("Order not found!");

    }

    private void DeleteOrderDetailAsync(Order order)
    {
        _unitOfWork.OrderDetailRepository.SoftRemoveRange(order.OrderDetails!);
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

        if (string.IsNullOrEmpty(model.ShippingAddress) && string.IsNullOrEmpty(customer.Address))
            throw new BadRequestException("Shipping Address is required");

        var order = await GetOrderByIdAsync(id);

        if (order.OrderStatus != OrderStatusConstants.PENDING || order.OrderStatus != OrderStatusConstants.CONFIRMED)
            throw new BadRequestException("Only update when status is PENDING!");

        DeleteOrderDetailAsync(order);

        _mapper.Map(model, order);

        var getOrderDetail = await GetOrderDetails(order.Id, model.OrderDetailCreateModels);
        order.TotalProductPrice = getOrderDetail.Item2;

        if (model.ShippingAddress != order.ShippingAddress)
            order = await CalculateShipping(bakery, order);

        if (order.VoucherCode != model.VoucherCode)
            order = await CalculateDiscount(order);

        order = CalculateFinalPrice(order);

        return order;

    }


    public async Task<Order> CancelAsync(Guid id)
    {
        var order = await GetOrderByIdAsync(id);

        order.OrderStatus = OrderStatusConstants.CANCELED;
        order.CancelBy = _claimsService.GetCurrentUserRole;

        return order;
    }

    public async Task<Order?> MoveToNextAsync<Order>(Guid id, List<IFormFile>? files = null)
    {
        var order = await GetOrderByIdAsync(id);

        var transitions = GetTransitions();

        if (!transitions.TryGetValue(order.OrderStatus!, out var transitionObj))
            throw new BadRequestException("Invalid state transition!");

        var transition = transitionObj as OrderStateTransition<Order> ?? throw new BadRequestException("Invalid state transition!");

        transition.Guard?.Invoke(order);

        transition.Validate?.Invoke((order, files)!);

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
