using CusCake.Application.Repositories;
using CusCake.Application.Services;
using CusCake.Application.Services.IServices;
using CusCake.Application.Utils;
using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CusCake.Infrastructures.Repositories;

public class GenericRepository<TEntity>(AppDbContext context, ICurrentTime currentTime, IClaimsService claimsService) : IGenericRepository<TEntity> where TEntity : BaseEntity
{

    protected DbSet<TEntity> _dbSet = context.Set<TEntity>();
    private readonly ICurrentTime _timeService = currentTime;
    private readonly IClaimsService _claimsService = claimsService;

    // Thêm phương thức riêng để xử lý sắp xếp
    private IQueryable<TEntity> ApplyOrderingAndFiltering(
        IQueryable<TEntity> query,
        bool withDeleted = false,
        Expression<Func<TEntity, bool>>? filter = null,
        List<(Expression<Func<TEntity, object>> OrderBy, bool IsDescending)>? orderByList = null)
    {
        // Áp dụng filter chung cho deleted entities
        query = query.Where(x => withDeleted || !x.IsDeleted);

        // Áp dụng filter tùy chỉnh nếu có
        if (filter != null)
        {
            query = query.Where(filter);
        }

        // Áp dụng sắp xếp
        IQueryable<TEntity> finalQuery;

        if (orderByList != null && orderByList.Count > 0)
        {
            var isFirstOrder = true;
            IOrderedQueryable<TEntity> orderedQuery = null!;

            foreach (var (orderBy, isDescending) in orderByList)
            {
                if (isFirstOrder)
                {
                    orderedQuery = isDescending
                        ? query.OrderByDescending(orderBy)
                        : query.OrderBy(orderBy);
                    isFirstOrder = false;
                }
                else
                {
                    orderedQuery = isDescending
                        ? orderedQuery.ThenByDescending(orderBy)
                        : orderedQuery.ThenBy(orderBy);
                }
            }

            finalQuery = orderedQuery;
        }
        else
        {
            // Mặc định sắp xếp theo CreatedAt giảm dần
            finalQuery = query.OrderByDescending(x => x.CreatedAt);
        }

        return finalQuery;
    }

    public async Task<List<TEntity>> GetAllAsync(
        bool withDeleted = false,
        List<(Expression<Func<TEntity, object>> OrderBy, bool IsDescending)>? orderByList = null,
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = includes
            .Aggregate(_dbSet.AsQueryable(),
                (entity, property) => entity.Include(property).IgnoreAutoIncludes());

        // Sử dụng phương thức chung cho việc sắp xếp
        var finalQuery = ApplyOrderingAndFiltering(query, withDeleted, null, orderByList);

        return await finalQuery.ToListAsync();
    }

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

    public async Task<(Pagination, List<TEntity>)> ToPagination(
        int pageIndex = 0,
        int pageSize = 10,
        bool withDeleted = false,
        Expression<Func<TEntity, bool>>? filter = null,
        List<(Expression<Func<TEntity, object>> OrderBy, bool IsDescending)>? orderByList = null,
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = includes
            .Aggregate(_dbSet.AsQueryable(),
                (entity, property) => entity.Include(property));

        // Sử dụng phương thức chung cho việc sắp xếp và lọc
        var finalQuery = ApplyOrderingAndFiltering(query, withDeleted, filter, orderByList);

        // Đếm tổng số items trước khi phân trang
        var totalCount = await finalQuery.CountAsync();

        // Áp dụng phân trang
        var paginatedItems = await finalQuery
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var pagination = new Pagination
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            TotalItemsCount = totalCount,
        };

        return (pagination, paginatedItems);
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

    public async Task<List<TEntity>> WhereAsync(
        Expression<Func<TEntity, bool>> filter,
        bool withDeleted = false,
        List<(Expression<Func<TEntity, object>> OrderBy, bool IsDescending)>? orderByList = null,
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = includes
            .Aggregate(_dbSet!.AsQueryable(),
                (entity, property) => entity.Include(property)).AsNoTracking();

        // Sử dụng phương thức chung cho việc sắp xếp và lọc
        var finalQuery = ApplyOrderingAndFiltering(query, withDeleted, filter, orderByList);

        return await finalQuery.ToListAsync();
    }

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
