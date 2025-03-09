using CusCake.Application.Repositories;

namespace CusCake.Application;

public interface IUnitOfWork
{
    IAuthRepository AuthRepository { get; }
    IAdminRepository AdminRepository { get; }
    IBakeryRepository BakeryRepository { get; }
    IAvailableCakeRepository AvailableCakeRepository { get; }
    IBankEventRepository BankEventRepository { get; }
    ICakeDecorationOptionRepository CakeDecorationOptionRepository { get; }
    // ICakeDecorationTypeRepository CakeDecorationTypeRepository { get; }
    ICakeDecorationSelectionRepository CakeDecorationSelectionRepository { get; }
    ICakeExtraOptionRepository CakeExtraOptionRepository { get; }
    ICakeExtraSelectionRepository CakeExtraSelectionRepository { get; }
    // ICakeExtraTypeRepository CakeExtraTypeRepository { get; }
    // ICakeMessageTypeRepository CakeMessageTypeRepository { get; }
    ICakeMessageOptionRepository CakeMessageOptionRepository { get; }
    ICakeMessageSelectionRepository CakeMessageSelectionRepository { get; }
    ICakePartSelectionRepository CakePartSelectionRepository { get; }
    // ICakePartTypeRepository CakePartTypeRepository { get; }
    ICakePartOptionRepository CakePartOptionRepository { get; }
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