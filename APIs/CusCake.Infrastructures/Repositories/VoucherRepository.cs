using CusCake.Application.Repositories;
using CusCake.Application.Services;
using CusCake.Application.Services.IServices;
using CusCake.Domain.Entities;

namespace CusCake.Infrastructures.Repositories
{
    public class VoucherRepository : GenericRepository<Voucher>, IVoucherRepository
    {
        public VoucherRepository(AppDbContext context, ICurrentTime currentTime, IClaimsService claimsService) : base(context, currentTime, claimsService)
        {
        }
    }
}
