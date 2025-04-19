using CusCake.Domain.Entities;

namespace CusCake.Application.Repositories
{
    public interface IBakeryRepository : IGenericRepository<Bakery>
    {
    }

    public interface IBakeryMetricRepository : IGenericRepository<BakeryMetric>
    {
    }
}

