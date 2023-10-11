using Microsoft.Extensions.Caching.Distributed;

namespace ServiceLibrary.Repository.Interfaces;

public interface ICacheRepository<TEntity, in TId>
    where TEntity : class
    where TId : notnull
{
    Task DeleteAsync(
        TId id,
        IEnumerable<string>? additionKeys = null,
        CancellationToken cancellationToken = default);

    Task<TEntity?> GetAsync(
        TId id,
        IEnumerable<string>? additionKeys = null,
        CancellationToken cancellationToken = default);

    Task SetAsync(
        TEntity entity,
        IEnumerable<string>? additionKeys = null,
        DistributedCacheEntryOptions? options = null,
        CancellationToken cancellationToken = default);
}