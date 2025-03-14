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
        var order = _mapper.Map<Order>(model);

        var getOrderDetail = await GetOrderDetails(order.Id, model.OrderDetailCreateModels);

        order.TotalPrice = getOrderDetail.Item2;
        order.CustomerId = _claimsService.GetCurrentUser;


        return null;
    }

    private double CalculateDistance(string origin, string destination)
    {
        return 0;
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


}
