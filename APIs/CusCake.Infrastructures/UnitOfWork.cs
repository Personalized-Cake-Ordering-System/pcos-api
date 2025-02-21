using CusCake.Application;
using CusCake.Application.Repositories;

namespace CusCake.Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDbContext _appDbContext;

        public UnitOfWork(
            AppDbContext appDbContext,
            ICustomerRepository customerRepository,
            IBakeryRepository bakeryRepository)
        {
            _appDbContext = appDbContext;
            CustomerRepository = customerRepository;
            BakeryRepository = bakeryRepository;
        }

        public ICustomerRepository CustomerRepository { get; }

        public IBakeryRepository BakeryRepository { get; }

        public async Task<bool> SaveChangesAsync() => (await _appDbContext.SaveChangesAsync()) > 0;
    }
}
