using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using ServiceLibrary.Repository.Interfaces;

namespace ServiceLibrary.Repository;

public class CacheRepository<TEntity, TId> : ICacheRepository<TEntity, TId>
    where TEntity : class
    where TId : notnull
{
    private readonly IDistributedCache _cache;
    private readonly string _entityTypeName = typeof(TEntity).Name;
    private readonly Func<TEntity, TId> _getId;
    private readonly ILogger<CacheRepository<TEntity, TId>> _logger;

    public CacheRepository(
        IDistributedCache cache,
        Func<TEntity, TId> getId,
        ILogger<CacheRepository<TEntity, TId>> logger)
        : this(cache, getId, logger, ":")
    {
    }

    protected CacheRepository(
        IDistributedCache cache,
        Func<TEntity, TId> getId,
        ILogger<CacheRepository<TEntity, TId>> logger,
        string separator)
    {
        _cache = cache;
        _getId = getId;
        _logger = logger;
        Separator = separator;
    }

    protected string Separator { get; }

    public async Task<TEntity?> GetAsync(
        TId id,
        IEnumerable<string>? additionKeys = null,
        CancellationToken cancellationToken = default)
    {
        var key = GetKey(id, additionKeys);

        var cachedValue = await _cache.GetStringAsync(key, cancellationToken);

        if (cachedValue is null)
        {
            return null;
        }

        var entity = DeSerialize(cachedValue);

        LogAction("Get", key);

        return entity;
    }

    public async Task SetAsync(
        TEntity entity,
        IEnumerable<string>? additionKeys = null,
        DistributedCacheEntryOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        var id = _getId(entity);
        var key = GetKey(id, additionKeys);
        var serializedObject = Serialize(entity);

        options ??= new DistributedCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromSeconds(60),
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
        };

        await _cache.SetStringAsync(key, serializedObject, options, cancellationToken);

        LogAction("Set", key);
    }

    public async Task DeleteAsync(
        TId id,
        IEnumerable<string>? additionKeys = null,
        CancellationToken cancellationToken = default)
    {
        var key = GetKey(id, additionKeys);
        await _cache.RemoveAsync(key, cancellationToken);

        LogAction("Delete", key);
    }

    protected virtual string GetKey(TId id, IEnumerable<string>? additionKeys = null)
    {
        if (additionKeys is null)
        {
            return string.Join(Separator, _entityTypeName, id.ToString());
        }

        var additionKeysString = string.Join(Separator, additionKeys.Where(x => !string.IsNullOrWhiteSpace(x)));

        return string.Join(Separator, _entityTypeName, additionKeysString, id.ToString());
    }

    protected virtual string Serialize(TEntity entity)
    {
        return JsonSerializer.Serialize(entity);
    }

    protected virtual TEntity? DeSerialize(string value)
    {
        return JsonSerializer.Deserialize<TEntity>(value);
    }

    private void LogAction(string action, string key)
    {
        _logger.LogInformation("{Action} '{EntityName}' in cache with key = '{Id}'", action, _entityTypeName, key);
    }
}