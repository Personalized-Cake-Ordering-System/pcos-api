using CusCake.Application.Utils;
using CusCake.Domain.Entities;
using System.Linq.Expressions;

namespace CusCake.Application.Repositories;
public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<List<TEntity>> GetAllAsync(bool withDeleted = false, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity?> GetByIdAsync(Guid id, bool withDeleted = false, params Expression<Func<TEntity, object>>[] includes);
    Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> expression, bool withDeleted = false, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool withDeleted = false, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity> AddAsync(TEntity entity);
    void Update(TEntity entity);
    void UpdateRange(List<TEntity> entities);
    void SoftRemove(TEntity entity);
    Task AddRangeAsync(List<TEntity> entities);
    void SoftRemoveRange(List<TEntity> entities);
    Task<(Pagination<TEntity>, List<TEntity>)> ToPagination(int pageNumber = 0, int pageSize = 10, bool withDeleted = false, params Expression<Func<TEntity, object>>[] includes);
}
