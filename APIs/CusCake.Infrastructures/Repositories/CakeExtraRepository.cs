using CusCake.Application.Repositories;
using CusCake.Application.Services;
using CusCake.Application.Services.IServices;
using CusCake.Domain.Entities;

namespace CusCake.Infrastructures.Repositories
{
    public class CakeExtraRepository : GenericRepository<CakeExtra>, ICakeExtraRepository
    {
        public CakeExtraRepository(AppDbContext context, ICurrentTime currentTime, IClaimsService claimsService) : base(context, currentTime, claimsService)
        {
        }
    }
}
