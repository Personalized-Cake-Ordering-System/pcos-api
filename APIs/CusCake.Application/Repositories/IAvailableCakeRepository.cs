using CusCake.Domain.Entities;

namespace CusCake.Application.Repositories
{
    public interface IAvailableCakeRepository : IGenericRepository<AvailableCake>
    {
    }

    public interface IAvailableCakeMetricRepository : IGenericRepository<AvailableCakeMetric>
    {
    }
}
