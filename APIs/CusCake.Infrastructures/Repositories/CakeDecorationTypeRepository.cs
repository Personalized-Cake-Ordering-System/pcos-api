using CusCake.Application.Repositories;
using CusCake.Application.Services;
using CusCake.Application.Services.IServices;
using CusCake.Domain.Entities;

namespace CusCake.Infrastructures.Repositories
{
    // public class CakeDecorationTypeRepository : GenericRepository<CakeDecorationType>, ICakeDecorationTypeRepository
    // {
    //     public CakeDecorationTypeRepository(AppDbContext context, ICurrentTime currentTime, IClaimsService claimsService) : base(context, currentTime, claimsService)
    //     {
    //     }
    // }

    public class CakeDecorationOptionRepository : GenericRepository<CakeDecorationOption>, ICakeDecorationOptionRepository
    {
        public CakeDecorationOptionRepository(AppDbContext context, ICurrentTime currentTime, IClaimsService claimsService) : base(context, currentTime, claimsService)
        {
        }
    }

    public class CakeDecorationSelectionRepository : GenericRepository<CakeDecorationSelection>, ICakeDecorationSelectionRepository
    {
        public CakeDecorationSelectionRepository(AppDbContext context, ICurrentTime currentTime, IClaimsService claimsService) : base(context, currentTime, claimsService)
        {
        }
    }
}
