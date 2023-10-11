using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ServiceLibrary.Repository;
using ServiceLibrary.Repository.Interfaces;

namespace ServiceLibrary.Extensions;

public static class CacheServiceExtension
{
    public static void AddCacheRepository<TEntity, TId>(
        this IServiceCollection services,
        Func<TEntity, TId> getId,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
        where TEntity : class
        where TId : notnull
    {
        services.AddCacheRepository<TEntity, TId>(service =>
        {
            var cacheRepository = GetImplementation(service, getId);

            return cacheRepository;
        }, lifetime);
    }

    private static void AddCacheRepository<TEntity, TId>(
        this IServiceCollection services,
        Func<IServiceProvider, object> implementationFactory,
        ServiceLifetime lifetime)
        where TEntity : class
        where TId : notnull
    {
        var serviceDescriptor =
            new ServiceDescriptor(typeof(ICacheRepository<TEntity, TId>), implementationFactory, lifetime);

        services.Add(serviceDescriptor);
    }

    private static ICacheRepository<TEntity, TId> GetImplementation<TEntity, TId>(
        IServiceProvider service,
        Func<TEntity, TId> getId)
        where TEntity : class
        where TId : notnull
    {
        var cache = service.GetRequiredService<IDistributedCache>();
        var logger = service.GetRequiredService<ILogger<CacheRepository<TEntity, TId>>>();

        return new CacheRepository<TEntity, TId>(cache, getId, logger);
    }
}