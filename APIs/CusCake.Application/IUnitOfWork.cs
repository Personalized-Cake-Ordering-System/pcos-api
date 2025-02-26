using CusCake.Application.Repositories;

namespace CusCake.Application;

public interface IUnitOfWork
{
    IBakeryRepository BakeryRepository { get; }
    IAvailableCakeRepository AvailableCakeRepository { get; }
    IBankEventRepository BankEventRepository { get; }
    ICakeDecorationDetailRepository CakeDecorationDetailRepository { get; }
    ICakeDecorationRepository CakeDecorationRepository { get; }
    ICakeExtraDetailRepository CakeExtraDetailRepository { get; }
    ICakeExtraRepository CakeExtraRepository { get; }
    ICakeMessageRepository CakeMessageRepository { get; }
    ICakePartDetailRepository CakePartDetailRepository { get; }
    ICakePartRepository CakePartRepository { get; }
    ICakeReviewRepository CakeReviewRepository { get; }
    ICustomCakeRepository CustomCakeRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    ICustomerVoucherRepository CustomerVoucherRepository { get; }
    INotificationRepository NotificationRepository { get; }
    IOrderDetailRepository OrderDetailRepository { get; }
    IOrderRepository OrderRepository { get; }
    IOrderSupportRepository OrderSupportRepository { get; }
    IStorageRepository StorageRepository { get; }
    ITransactionRepository TransactionRepository { get; }
    IVoucherRepository VoucherRepository { get; }
    Task<bool> SaveChangesAsync();
}