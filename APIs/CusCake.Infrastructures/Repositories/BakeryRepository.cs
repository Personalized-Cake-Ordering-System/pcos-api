using CusCake.Application.Repositories;
using CusCake.Application.Services;
using CusCake.Application.Services.IServices;
using CusCake.Domain.Entities;

namespace CusCake.Infrastructures.Repositories
{
    public class BakeryRepository(AppDbContext context, ICurrentTime currentTime, IClaimsService claimsService) : GenericRepository<Bakery>(context, currentTime, claimsService), IBakeryRepository
    {
    }

    public class BakeryMetricRepository(AppDbContext context, ICurrentTime currentTime, IClaimsService claimsService) : GenericRepository<BakeryMetric>(context, currentTime, claimsService), IBakeryMetricRepository
    {
    }
}
