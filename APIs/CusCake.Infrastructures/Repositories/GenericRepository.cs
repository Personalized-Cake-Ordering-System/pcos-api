

using CusCake.Application.Repositories;
using CusCake.Application.Services;
using CusCake.Application.Services.IServices;
using CusCake.Application.Utils;
using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CusCake.Infrastructures.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{

    protected DbSet<TEntity> _dbSet;
    private readonly ICurrentTime _timeService;
    private readonly IClaimsService _claimsService;

    public AppDbContext Context { get; }

    public GenericRepository(AppDbContext context, ICurrentTime currentTime, IClaimsService claimsService)
    {
        _dbSet = context.Set<TEntity>();
        _timeService = currentTime;
        _claimsService = claimsService;
    }

    public async Task<List<TEntity>> GetAllAsync(bool withDeleted = false, params Expression<Func<TEntity, object>>[] includes) =>
          await includes
         .Aggregate(_dbSet.AsQueryable(),
             (entity, property) => entity.Include(property).IgnoreAutoIncludes())
         .Where(x => withDeleted || !x.IsDeleted)
         .OrderByDescending(x => x.CreatedAt)
         .ToListAsync();

    public async Task<TEntity?> GetByIdAsync(Guid id, bool withDeleted = false, params Expression<Func<TEntity, object>>[] includes)
    {
        return await includes
            .Aggregate(_dbSet.AsQueryable(),
               (entity, property) => entity.Include(property))
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(id) && (withDeleted || !x.IsDeleted));
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        entity.CreatedAt = _timeService.GetCurrentTime();
        entity.CreatedBy = _claimsService.GetCurrentUser;
        var result = await _dbSet.AddAsync(entity);
        return result.Entity;
    }

    public void SoftRemove(TEntity entity)
    {
        entity.IsDeleted = true;
        entity.UpdatedBy = _claimsService.GetCurrentUser;
        entity.UpdatedAt = _timeService.GetCurrentTime();
        _dbSet.Update(entity);
    }

    public void Update(TEntity entity)
    {
        entity.UpdatedAt = _timeService.GetCurrentTime();
        entity.UpdatedBy = _claimsService.GetCurrentUser;
        _dbSet.Update(entity);
    }

    public void SoftRemoveRange(List<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.IsDeleted = true;
            entity.UpdatedAt = _timeService.GetCurrentTime();
            entity.UpdatedBy = _claimsService.GetCurrentUser;
        }
        _dbSet.UpdateRange(entities);
    }

    public async Task<Pagination<TEntity>> ToPagination(int pageIndex = 0, int pageSize = 10, bool withDeleted = false, params Expression<Func<TEntity, object>>[] includes)
    {
        var items = await GetAllAsync(withDeleted, includes);
        var result = new Pagination<TEntity>()
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            TotalItemsCount = items.Count,
            Items = (ICollection<TEntity>)items.Skip(pageIndex * pageSize)
                                .Take(pageSize),
        };

        return result;
    }

    public void UpdateRange(List<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.CreatedAt = _timeService.GetCurrentTime();
            entity.CreatedBy = _claimsService.GetCurrentUser;
        }
        _dbSet.UpdateRange(entities);
    }

    public async Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> expression, bool withDeleted = false, params Expression<Func<TEntity, object>>[] includes)
          => await includes
            .Aggregate(_dbSet!.AsQueryable(),
                (entity, property) => entity.Include(property)).AsNoTracking()
            .Where(expression!)
            .Where(x => withDeleted || !x.IsDeleted)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool withDeleted = false, params Expression<Func<TEntity, object>>[] includes)
         => await includes
        .Aggregate(_dbSet!.AsQueryable(),
            (entity, property) => entity!.Include(property)).AsNoTracking()
        .Where(expression!)
        .FirstOrDefaultAsync(x => withDeleted || !x.IsDeleted);


    public async Task AddRangeAsync(List<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.CreatedAt = _timeService.GetCurrentTime();
            entity.CreatedBy = _claimsService.GetCurrentUser;
        }
        await _dbSet.AddRangeAsync(entities);
    }
}
