using AutoMapper;
using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.ViewModels.TransactionModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using Hangfire;
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
    IBackgroundJobClient backgroundJobClient
) : ITransactionService
{
    private readonly IBackgroundJobClient _backgroundJobClient = backgroundJobClient;
    private readonly INotificationService _notificationService = notificationService;
    private readonly IWalletService _walletService = walletService;
    private readonly IAuthService _authService = authService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IOrderService _orderService = orderService;

    public async Task HandlerWebhookEvent(TransactionWebhookModel model)
    {
        var order = await _unitOfWork.OrderRepository.FirstOrDefaultAsync(x => model.Content!.Contains(x.OrderCode))
                        ?? throw new BadRequestException("Order is not found!");

        await CreateTransactionAsync(model, order);

        await MakeBillingAsync(order);

        order = await _orderService.MoveToNextAsync<Order>(order.Id);

        await CreateNotificationAsync(order!);

        var localExecuteTime = DateTime.Now.AddMinutes(5);
        var delay = localExecuteTime - DateTime.Now;
        _backgroundJobClient.Schedule(() => _orderService.BakeryConfirmAsync(order!.Id), delay);

    }


    private async Task CreateTransactionAsync(TransactionWebhookModel model, Order order)
    {
        var transaction = _mapper.Map<Transaction>(model);
        transaction.OrderId = order.Id;
        transaction.Amount = transaction.TransferAmount!.Value;
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
        var adminWallet = (await _authService.GetAdminAsync()).Wallet;

        await _walletService.MakeBillingAsync(adminWallet, order.TotalCustomerPaid, WalletTransactionTypeConstants.PENDING_PAYMENT);
    }

}
