using CusCake.Application.Repositories;
using CusCake.Application.Services;
using CusCake.Application.Services.IServices;
using CusCake.Domain.Entities;

namespace CusCake.Infrastructures.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(AppDbContext context, ICurrentTime currentTime, IClaimsService claimsService) : base(context, currentTime, claimsService)
        {
        }
    }
}
