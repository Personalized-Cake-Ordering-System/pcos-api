using CusCake.Application.Repositories;
using CusCake.Application.Services;
using CusCake.Application.Services.IServices;
using CusCake.Domain.Entities;

namespace CusCake.Infrastructures.Repositories
{
    public class AvailableCakeRepository : GenericRepository<AvailableCake>, IAvailableCakeRepository
    {
        public AvailableCakeRepository(AppDbContext context, ICurrentTime currentTime, IClaimsService claimsService) : base(context, currentTime, claimsService)
        {
        }
    }
    public class AvailableCakeMetricRepository : GenericRepository<AvailableCakeMetric>, IAvailableCakeMetricRepository
    {
        public AvailableCakeMetricRepository(AppDbContext context, ICurrentTime currentTime, IClaimsService claimsService) : base(context, currentTime, claimsService)
        {
        }
    }
}
