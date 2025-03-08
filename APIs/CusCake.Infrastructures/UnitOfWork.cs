using CusCake.Application;
using CusCake.Application.Repositories;

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
            IBankEventRepository bankEventRepository,
            ICakeDecorationDetailRepository cakeDecorationDetailRepository,
            ICakeDecorationRepository cakeDecorationRepository,
            ICakeExtraDetailRepository cakeExtraDetailRepository,
            ICakeExtraRepository cakeExtraRepository,
            ICakeMessageRepository cakeMessageRepository,
            ICakePartDetailRepository cakePartDetailRepository,
            ICakePartRepository cakePartRepository,
            ICakeReviewRepository cakeReviewRepository,
            ICustomCakeRepository customCakeRepository,
            ICustomerVoucherRepository customerVoucherRepository,
            INotificationRepository notificationRepository,
            IOrderDetailRepository orderDetailRepository,
            IOrderRepository orderRepository,
            IOrderSupportRepository orderSupportRepository,
            ITransactionRepository transactionRepository,
            IVoucherRepository voucherRepository,
            ICakeMessageTypeRepository cakeMessageTypeRepository,
            ICakeMessageDetailRepository cakeMessageDetailRepository,
            IAuthRepository authRepository
        )
        {
            _appDbContext = appDbContext;
            CustomerRepository = customerRepository;
            BakeryRepository = bakeryRepository;
            StorageRepository = storageRepository;
            AvailableCakeRepository = availableCakeRepository;
            BankEventRepository = bankEventRepository;
            CakeDecorationDetailRepository = cakeDecorationDetailRepository;
            CakeDecorationRepository = cakeDecorationRepository;
            CakeExtraDetailRepository = cakeExtraDetailRepository;
            CakeExtraRepository = cakeExtraRepository;
            CakeMessageRepository = cakeMessageRepository;
            CakePartDetailRepository = cakePartDetailRepository;
            CakePartRepository = cakePartRepository;
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
            CakeMessageDetailRepository = cakeMessageDetailRepository;
            CakeMessageTypeRepository = cakeMessageTypeRepository;
            AuthRepository = authRepository;
        }

        public ICustomerRepository CustomerRepository { get; }

        public IBakeryRepository BakeryRepository { get; }

        public IStorageRepository StorageRepository { get; }

        public IAvailableCakeRepository AvailableCakeRepository { get; }

        public IBankEventRepository BankEventRepository { get; }

        public ICakeDecorationDetailRepository CakeDecorationDetailRepository { get; }

        public ICakeDecorationRepository CakeDecorationRepository { get; }

        public ICakeExtraDetailRepository CakeExtraDetailRepository { get; }

        public ICakeExtraRepository CakeExtraRepository { get; }

        public ICakeMessageRepository CakeMessageRepository { get; }

        public ICakePartDetailRepository CakePartDetailRepository { get; }

        public ICakePartRepository CakePartRepository { get; }

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

        public ICakeMessageTypeRepository CakeMessageTypeRepository { get; }

        public ICakeMessageDetailRepository CakeMessageDetailRepository { get; }

        public IAuthRepository AuthRepository { get; }

        public async Task<bool> SaveChangesAsync() => (await _appDbContext.SaveChangesAsync()) > 0;
    }
}
