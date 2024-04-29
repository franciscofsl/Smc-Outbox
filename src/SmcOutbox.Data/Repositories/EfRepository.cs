using Microsoft.EntityFrameworkCore;
using SmcOutbox.Core.Common;

namespace SmcOutbox.Data.Repositories;

public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : Aggregate
{
    public EfRepository(AppDbContext context)
    {
        DbContext = context;
    }

    protected AppDbContext DbContext { get; }

    public async Task<TEntity> InsertAsync(TEntity entity, bool save = true)
    {
        await DbContext.Set<TEntity>().AddAsync(entity);
        if (save)
        {
            await DbContext.SaveChangesAsync();
        }

        return entity;
    }

    public async Task<TEntity> GetAsync(Guid entityId)
    {
        return await DbContext.Set<TEntity>().FirstOrDefaultAsync(_ => _.Id == entityId);
    }

    public async Task UpdateAsync(TEntity entity, bool save = true)
    {
        DbContext.Set<TEntity>().Update(entity);
        if (save)
        {
            await DbContext.SaveChangesAsync();
        }
    }
}