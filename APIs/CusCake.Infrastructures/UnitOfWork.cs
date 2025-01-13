using CusCake.Application;
using CusCake.Application.Repositories;

namespace CusCake.Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDbContext _appDbContext;

        public UnitOfWork(
            AppDbContext appDbContext,
            ICustomerRepository customerRepository)
        {
            _appDbContext = appDbContext;
            CustomerRepository= customerRepository;
            
        }

        public ICustomerRepository CustomerRepository { get; }

        public async Task<bool> SaveChangesAsync() => (await _appDbContext.SaveChangesAsync()) > 0;
    }
}
