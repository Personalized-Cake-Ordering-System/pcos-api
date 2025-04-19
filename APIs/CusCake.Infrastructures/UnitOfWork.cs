using CusCake.Application;
using CusCake.Application.Repositories;
using CusCake.Domain.Entities;

namespace CusCake.Infrastructures
{
    public class UnitOfWork(
        AppDbContext appDbContext,
        IAdminRepository adminRepository,
        ICustomerRepository customerRepository,
        IBakeryRepository bakeryRepository,
        IStorageRepository storageRepository,
        IAvailableCakeRepository availableCakeRepository,

        ICakeDecorationOptionRepository cakeDecorationOptionRepository,
        // ICakeDecorationTypeRepository cakeDecorationTypeRepository,
        ICakeDecorationSelectionRepository cakeDecorationSelectionRepository,

        ICakeExtraOptionRepository cakeExtraOptionRepository,
        ICakeExtraSelectionRepository cakeExtraSelectionRepository,
        // ICakeExtraTypeRepository cakeExtraTypeRepository,

        ICakeMessageOptionRepository cakeMessageOptionRepository,
        ICakeMessageSelectionRepository cakeMessageSelectionRepository,
        // ICakeMessageTypeRepository cakeMessageTypeRepository,

        ICakePartOptionRepository cakePartOptionRepository,
        ICakePartSelectionRepository cakePartSelectionRepository,
        // ICakePartTypeRepository cakePartTypeRepository,

        IReviewRepository reviewRepository,
        ICustomCakeRepository customCakeRepository,
        ICustomerVoucherRepository customerVoucherRepository,
        INotificationRepository notificationRepository,
        IOrderDetailRepository orderDetailRepository,
        IOrderRepository orderRepository,
        IOrderSupportRepository orderSupportRepository,
        ITransactionRepository transactionRepository,
        IVoucherRepository voucherRepository,
        IAuthRepository authRepository,
        IWalletRepository walletRepository,
        IWalletTransactionRepository walletTransaction,
        IMongoRepository mongoRepository,
        IReportRepository reportRepository,
        IBakeryMetricRepository bakeryMetricRepository,
        IAvailableCakeMetricRepository availableCakeMetricRepository
        ) : IUnitOfWork
    {

        private readonly AppDbContext _appDbContext = appDbContext;

        public ICustomerRepository CustomerRepository { get; } = customerRepository;

        public IBakeryRepository BakeryRepository { get; } = bakeryRepository;

        public IStorageRepository StorageRepository { get; } = storageRepository;

        public IAvailableCakeRepository AvailableCakeRepository { get; } = availableCakeRepository;

        public IReviewRepository ReviewRepository { get; } = reviewRepository;

        public ICustomCakeRepository CustomCakeRepository { get; } = customCakeRepository;

        public ICustomerVoucherRepository CustomerVoucherRepository { get; } = customerVoucherRepository;

        public INotificationRepository NotificationRepository { get; } = notificationRepository;
        public IOrderDetailRepository OrderDetailRepository { get; } = orderDetailRepository;
        public IOrderRepository OrderRepository { get; } = orderRepository;
        public IOrderSupportRepository OrderSupportRepository { get; } = orderSupportRepository;
        public ITransactionRepository TransactionRepository { get; } = transactionRepository;
        public IVoucherRepository VoucherRepository { get; } = voucherRepository;
        public IAdminRepository AdminRepository { get; } = adminRepository;

        // public ICakeMessageTypeRepository CakeMessageTypeRepository { get; }

        public IAuthRepository AuthRepository { get; } = authRepository;

        public ICakePartSelectionRepository CakePartSelectionRepository { get; } = cakePartSelectionRepository;

        // public ICakePartTypeRepository CakePartTypeRepository { get; }

        public ICakePartOptionRepository CakePartOptionRepository { get; } = cakePartOptionRepository;

        public ICakeDecorationOptionRepository CakeDecorationOptionRepository { get; } = cakeDecorationOptionRepository;

        // public ICakeDecorationTypeRepository CakeDecorationTypeRepository { get; }

        public ICakeDecorationSelectionRepository CakeDecorationSelectionRepository { get; } = cakeDecorationSelectionRepository;

        public ICakeMessageOptionRepository CakeMessageOptionRepository { get; } = cakeMessageOptionRepository;

        public ICakeMessageSelectionRepository CakeMessageSelectionRepository { get; } = cakeMessageSelectionRepository;

        public ICakeExtraOptionRepository CakeExtraOptionRepository { get; } = cakeExtraOptionRepository;

        public ICakeExtraSelectionRepository CakeExtraSelectionRepository { get; } = cakeExtraSelectionRepository;

        public IWalletRepository WalletRepository { get; } = walletRepository;

        public IWalletTransactionRepository WalletTransaction { get; } = walletTransaction;

        public IMongoRepository MongoRepository { get; } = mongoRepository;

        public IReportRepository ReportRepository { get; } = reportRepository;

        public IBakeryMetricRepository BakeryMetricRepository => bakeryMetricRepository;

        public IAvailableCakeMetricRepository AvailableCakeMetricRepository => availableCakeMetricRepository;

        // public ICakeExtraTypeRepository CakeExtraTypeRepository { get; }

        public async Task<bool> SaveChangesAsync() => (await _appDbContext.SaveChangesAsync()) > 0;
    }
}
