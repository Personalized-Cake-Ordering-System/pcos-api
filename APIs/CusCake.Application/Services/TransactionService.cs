using AutoMapper;
using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.ViewModels.TransactionModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using Newtonsoft.Json;

namespace CusCake.Application.Services;

public interface ITransactionService
{
    Task HandlerWebhookEvent(TransactionWebhookModel model);
}

public class TransactionService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IOrderService orderService,
    IAuthService authService,
    IWalletService walletService,
    INotificationService notificationService,
    IHangfireService hangfireService
) : ITransactionService
{
    private readonly INotificationService _notificationService = notificationService;
    private readonly IWalletService _walletService = walletService;
    private readonly IAuthService _authService = authService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IOrderService _orderService = orderService;
    private readonly IHangfireService _hangfireService = hangfireService;

    public async Task HandlerWebhookEvent(TransactionWebhookModel model)
    {
        var order = await _unitOfWork.OrderRepository.FirstOrDefaultAsync(x => model.Content!.Contains(x.OrderCode))
                        ?? throw new BadRequestException("Order is not found!");
        var transaction = _mapper.Map<Transaction>(model);

        await CreateTransactionAsync(transaction, order.Id);

        await MakeBillingAsync(order);

        order = await _orderService.MoveToNextAsync<Order>(order.Id);

        await CreateNotificationAsync(order!);

        _hangfireService.ScheduleJob(new JobRequest
        {
            Action = async () => await _orderService.BakeryConfirmAsync(order!.Id),
            ExecuteTime = DateTime.Now.AddMinutes(5),
        });

    }


    private async Task CreateTransactionAsync(Transaction transaction, Guid orderId)
    {
        transaction.OrderId = orderId;
        await _unitOfWork.TransactionRepository.AddAsync(transaction);
        await _unitOfWork.SaveChangesAsync();

    }

    private async Task CreateNotificationAsync(Order order)
    {
        var orderJson = JsonConvert.SerializeObject(order);

        await _notificationService.CreateOrderNotificationAsync(order.Id, NotificationType.PAYMENT_SUCCESS, null, order.CustomerId);
        await _notificationService.CreateOrderNotificationAsync(order.Id, NotificationType.NEW_ORDER, order.BakeryId, null);

        await _notificationService.SendNotificationAsync(order.CustomerId, orderJson, NotificationType.PAYMENT_SUCCESS);
        await _notificationService.SendNotificationAsync(order.BakeryId, orderJson, NotificationType.NEW_ORDER);

    }

    private async Task MakeBillingAsync(Order order)
    {
        var customerWallet = (await _authService.GetAuthByIdAsync(order.CustomerId)).Wallet;
        var bakeryWallet = (await _authService.GetAuthByIdAsync(order.BakeryId)).Wallet;

        await _walletService.MakeBillingAsync(customerWallet, order.ShopRevenue);
        await _walletService.MakeBillingAsync(bakeryWallet, order.AppCommissionFee);
    }

}
