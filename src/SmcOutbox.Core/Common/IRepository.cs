namespace SmcOutbox.Core.Common;

public interface IRepository<TEntity> where TEntity : Aggregate
{
    Task<TEntity> InsertAsync(TEntity entity, bool save = true);

    Task<TEntity> GetAsync(Guid entityId);

    Task UpdateAsync(TEntity entity, bool save = true);
}