using CusCake.Application.Repositories;
using CusCake.Application.Services;
using CusCake.Application.Services.IServices;
using CusCake.Domain.Entities;

namespace CusCake.Infrastructures.Repositories
{
    // public class CakeExtraTypeRepository : GenericRepository<CakeExtraType>, ICakeExtraTypeRepository
    // {
    //     public CakeExtraTypeRepository(AppDbContext context, ICurrentTime currentTime, IClaimsService claimsService) : base(context, currentTime, claimsService)
    //     {
    //     }
    // }
    public class CakeExtraOptionRepository : GenericRepository<CakeExtraOption>, ICakeExtraOptionRepository
    {
        public CakeExtraOptionRepository(AppDbContext context, ICurrentTime currentTime, IClaimsService claimsService) : base(context, currentTime, claimsService)
        {
        }
    }
    public class CakeExtraSelectionRepository : GenericRepository<CakeExtraSelection>, ICakeExtraSelectionRepository
    {
        public CakeExtraSelectionRepository(AppDbContext context, ICurrentTime currentTime, IClaimsService claimsService) : base(context, currentTime, claimsService)
        {
        }
    }
}
