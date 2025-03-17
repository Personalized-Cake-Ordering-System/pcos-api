using CusCake.Application;
using CusCake.Application.Repositories;
using CusCake.Domain.Entities;

namespace CusCake.Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDbContext _appDbContext;

        public UnitOfWork(
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

            ICakeReviewRepository cakeReviewRepository,
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
            IWalletTransactionRepository walletTransaction
        )
        {
            _appDbContext = appDbContext;
            CustomerRepository = customerRepository;
            BakeryRepository = bakeryRepository;
            StorageRepository = storageRepository;
            AvailableCakeRepository = availableCakeRepository;

            CakeExtraOptionRepository = cakeExtraOptionRepository;
            CakeExtraSelectionRepository = cakeExtraSelectionRepository;
            // CakeExtraTypeRepository = cakeExtraTypeRepository;

            CakeDecorationOptionRepository = cakeDecorationOptionRepository;
            CakeDecorationSelectionRepository = cakeDecorationSelectionRepository;
            // CakeDecorationTypeRepository = cakeDecorationTypeRepository;

            // CakeMessageTypeRepository = cakeMessageTypeRepository;
            CakeMessageOptionRepository = cakeMessageOptionRepository;
            CakeMessageSelectionRepository = cakeMessageSelectionRepository;

            CakePartSelectionRepository = cakePartSelectionRepository;
            CakePartOptionRepository = cakePartOptionRepository;
            // CakePartTypeRepository = cakePartTypeRepository;

            CakeReviewRepository = cakeReviewRepository;
            CustomCakeRepository = customCakeRepository;
            CustomerVoucherRepository = customerVoucherRepository;
            NotificationRepository = notificationRepository;
            OrderDetailRepository = orderDetailRepository;
            OrderRepository = orderRepository;
            OrderSupportRepository = orderSupportRepository;
            TransactionRepository = transactionRepository;
            VoucherRepository = voucherRepository;
            AdminRepository = adminRepository;
            AuthRepository = authRepository;
            WalletRepository = walletRepository;
            WalletTransaction = walletTransaction;
        }

        public ICustomerRepository CustomerRepository { get; }

        public IBakeryRepository BakeryRepository { get; }

        public IStorageRepository StorageRepository { get; }

        public IAvailableCakeRepository AvailableCakeRepository { get; }

        public ICakeReviewRepository CakeReviewRepository { get; }

        public ICustomCakeRepository CustomCakeRepository { get; }

        public ICustomerVoucherRepository CustomerVoucherRepository { get; }

        public INotificationRepository NotificationRepository { get; }
        public IOrderDetailRepository OrderDetailRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IOrderSupportRepository OrderSupportRepository { get; }
        public ITransactionRepository TransactionRepository { get; }
        public IVoucherRepository VoucherRepository { get; }
        public IAdminRepository AdminRepository { get; }

        // public ICakeMessageTypeRepository CakeMessageTypeRepository { get; }

        public IAuthRepository AuthRepository { get; }

        public ICakePartSelectionRepository CakePartSelectionRepository { get; }

        // public ICakePartTypeRepository CakePartTypeRepository { get; }

        public ICakePartOptionRepository CakePartOptionRepository { get; }

        public ICakeDecorationOptionRepository CakeDecorationOptionRepository { get; }

        // public ICakeDecorationTypeRepository CakeDecorationTypeRepository { get; }

        public ICakeDecorationSelectionRepository CakeDecorationSelectionRepository { get; }

        public ICakeMessageOptionRepository CakeMessageOptionRepository { get; }

        public ICakeMessageSelectionRepository CakeMessageSelectionRepository { get; }

        public ICakeExtraOptionRepository CakeExtraOptionRepository { get; }

        public ICakeExtraSelectionRepository CakeExtraSelectionRepository { get; }

        public IWalletRepository WalletRepository { get; }

        public IWalletTransactionRepository WalletTransaction { get; }

        // public ICakeExtraTypeRepository CakeExtraTypeRepository { get; }

        public async Task<bool> SaveChangesAsync() => (await _appDbContext.SaveChangesAsync()) > 0;
    }
}
