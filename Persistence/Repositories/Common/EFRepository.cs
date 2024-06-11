using Application.Repositories.Common;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Common;
public class EFRepository<TEntity> : IRepository<TEntity>
where TEntity : Entity
{
    protected DbContext Context { get; }

    public EFRepository(DbContext context)
    {
        Context = context;
    }
    public IQueryable<TEntity> Query() => Context.Set<TEntity>();

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>?
                                                           include = null, bool tracking = true)
    {
        var queryable = Query();

        if (!tracking) queryable = queryable.AsNoTracking();
        if (include is not null) queryable = include(queryable);

        return await queryable.FirstOrDefaultAsync(predicate);
    }

    public async Task<IList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>?
                                                           include = null, bool tracking = true, int index = 0, int size = 10)
    {
        var queryable = Query();

        if (!tracking) queryable = queryable.AsNoTracking();
        if (include is not null) queryable = include(queryable);
        if (predicate is not null) queryable = Query().Where(predicate);
        var a = await queryable.Skip(index * size).Take(size).ToListAsync();
        return await queryable.Skip(index * size).Take(size).ToListAsync();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        entity.CreatedDate = DateTime.UtcNow;
        await Context.AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity;
    }
    public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities)
    {
        foreach (TEntity entity in entities)
            entity.CreatedDate = DateTime.UtcNow;
        await Context.AddRangeAsync(entities);
        await Context.SaveChangesAsync();
        return entities;
    }
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        entity.UpdatedDate = DateTime.UtcNow;
        Context.Update(entity);
        await Context.SaveChangesAsync();
        return entity;
    }
    public async Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities)
    {
        foreach (TEntity entity in entities)
            entity.UpdatedDate = DateTime.UtcNow;
        Context.UpdateRange(entities);
        await Context.SaveChangesAsync();
        return entities;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        Context.Remove(entity);
        await Context.SaveChangesAsync();
        return entity;
    }
    public async Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities)
    {
        Context.RemoveRange(entities);
        await Context.SaveChangesAsync();
        return entities;
    }
}
