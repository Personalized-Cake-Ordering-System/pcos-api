using System.Threading.Tasks;
using AutoMapper;
using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.Services.IServices;
using CusCake.Application.ViewModels.OrderModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;

public interface IOrderService
{
    Task<Order> CreateAsync(OrderCreateModel model);
}

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IClaimsService _claimsService;
    private readonly ICustomerService _customerService;
    private readonly IBakeryService _bakeryService;
    private readonly IAvailableCakeService _availableCakeService;
    public OrderService(IUnitOfWork unitOfWork,
        IMapper mapper,
        IBakeryService bakeryService,
        ICustomerService customerService,
        IClaimsService claimsService,
        IAvailableCakeService availableCakeService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _bakeryService = bakeryService;
        _customerService = customerService;
        _claimsService = claimsService;
        _availableCakeService = availableCakeService;
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
        model.PhoneNumber ??= customer.Phone;
        model.OrderAddress ??= customer.Address;

        var getOrderDetail = await GetOrderDetails(model.OrderDetailCreateModels);

        var order = _mapper.Map<Order>(model);
        order.TotalPrice = getOrderDetail.Item2;
        order.OrderDetails = getOrderDetail.Item1;
        order.CustomerId = _claimsService.GetCurrentUser;


        return null;
    }

    private async Task<(List<OrderDetail>, double)> GetOrderDetails(List<OrderDetailCreateModel> orderDetails)
    {
        var details = _mapper.Map<List<OrderDetail>>(orderDetails);
        double total = 0;
        for (int i = 0; i < orderDetails.Count; i++)
        {
            var available_cake_id = orderDetails[i].AvailableCakeId;
            var customer_cake_id = orderDetails[i].CustomCakeId;
            if (available_cake_id.HasValue)
            {
                var availableCake = await _availableCakeService.GetByIdAsync(available_cake_id.Value);
                total += availableCake.AvailableCakePrice;
                details[i].SubTotalPrice = availableCake.AvailableCakePrice;
            }
            else
            {
                // handle custom cake
            }
        }

        return (details, total);
    }
}
