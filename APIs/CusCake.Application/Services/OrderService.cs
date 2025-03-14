using AutoMapper;
using CusCake.Application.Extensions;
using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.Services.IServices;
using CusCake.Application.Utils;
using CusCake.Application.ViewModels.OrderModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;

public interface IOrderService
{
    Task<Order> CreateAsync(OrderCreateModel model);
    Task<Order> GetOrderByIdAsync(Guid id);
    Task<Order> UpdateAsync(Guid id, OrderUpdateModel model);
}

public class OrderService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ICustomerService customerService,
    IClaimsService claimsService,
    IGoongService goongService,
    IVoucherService voucherService
) : IOrderService
{
    private readonly IVoucherService _voucherService = voucherService;
    private readonly IGoongService _goongService = goongService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IClaimsService _claimsService = claimsService;
    private readonly ICustomerService _customerService = customerService;

    public Dictionary<string, OrderStateTransition> GetTransitions()
    {
        return new Dictionary<string, OrderStateTransition>
        {
            {
                "ConfirmOrder", new OrderStateTransition
                {
                    From = [OrderStatusConstants.PENDING],
                    To = OrderStatusConstants.CONFIRMED,
                    Guard = (customerId) =>
                    {
                        return _claimsService.GetCurrentUserRole == RoleConstants.CUSTOMER;
                    }
                }
            },
            {
                "MarkPaymentPending", new OrderStateTransition
                {
                    From = [OrderStatusConstants.CONFIRMED],
                    To = OrderStatusConstants.PAYMENT_PENDING,
                    // Guard = async (orderId) =>
                    // {
                    //     var user = await _userService.GetCurrentUserAsync();
                    //     return user != null;
                    // }
                }
            },
            {
                "MarkAsPaid", new OrderStateTransition
                {
                    From = [OrderStatusConstants.PAYMENT_PENDING],
                    To = OrderStatusConstants.PAID,
                    // Guard = async (orderId) =>
                    // {
                    //     var user = await _userService.GetCurrentUserAsync();
                    //     return user != null;
                    // }
                }
            },
            {
                "StartProcessing", new OrderStateTransition
                {
                    From = [OrderStatusConstants.PAID],
                    To = OrderStatusConstants.PROCESSING
                }
            },
            {
                "ReadyForPickup", new OrderStateTransition
                {
                    From = [OrderStatusConstants.PROCESSING],
                    To = OrderStatusConstants.READY_FOR_PICKUP
                }
            },
            {
                "CustomerConfirm", new OrderStateTransition
                {
                    From = [OrderStatusConstants.READY_FOR_PICKUP],
                    To = OrderStatusConstants.CUSTOMER_CONFIRMED
                }
            },
            {
                "ShipOrder", new OrderStateTransition
                {
                    From = [OrderStatusConstants.CUSTOMER_CONFIRMED],
                    To = OrderStatusConstants.SHIPPING
                }
            },
            {
                "CompleteOrder", new OrderStateTransition
                {
                    From = [OrderStatusConstants.SHIPPING],
                    To = OrderStatusConstants.COMPLETED
                }
            },
            {
                "AutoCompleteOrder", new OrderStateTransition
                {
                    From = [OrderStatusConstants.READY_FOR_PICKUP, OrderStatusConstants.SHIPPING],
                    To = OrderStatusConstants.AUTO_COMPLETED
                }
            },
            {
                "CancelOrder", new OrderStateTransition
                {
                    From =
                    [
                        OrderStatusConstants.PENDING,
                        OrderStatusConstants.CONFIRMED,
                        OrderStatusConstants.PAYMENT_PENDING,
                        OrderStatusConstants.PAID,
                        OrderStatusConstants.PROCESSING,
                        OrderStatusConstants.READY_FOR_PICKUP
                    ],
                    To = OrderStatusConstants.CANCELED,
                    // Guard = async (orderId) =>
                    // {
                    //     var user = await _userService.GetCurrentUserAsync();
                    //     return user != null && user.Role == "ADMIN"; // Chỉ admin có quyền hủy
                    // }
                }
            }
        };
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

        var order = _mapper.Map<Order>(model);
        order.CustomerId = _claimsService.GetCurrentUser;
        var getOrderDetail = await GetOrderDetails(order.Id, model.OrderDetailCreateModels);
        order.TotalProductPrice = getOrderDetail.Item2;

        order = await CalculateShipping(bakery, order);

        order = await CalculateDiscount(order);

        order = CalculateFinalPrice(order);

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
        if (order.ShippingType == ShippingTypeConstants.PICK_UP)
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
            order.Longitude!, order.Longitude!
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

    public async Task<Order> GetOrderByIdAsync(Guid id)
    {
        var includes = QueryHelper.Includes<Order>(
            x => x.Customer!,
            x => x.Bakery!,
            x => x.Transaction!,
            x => x.Voucher!,
            x => x.CustomerVoucher!);

        var order = await _unitOfWork.OrderRepository.GetByIdAsync(id, includes: includes)
            ?? throw new BadRequestException("Order not found!");

        order.OrderDetails = await _unitOfWork.OrderDetailRepository.WhereAsync(x => x.OrderId == id);

        return order;
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

        if (order.OrderStatus != OrderStatusConstants.PENDING)
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
}
