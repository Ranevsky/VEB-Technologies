using Microsoft.Extensions.DependencyInjection;

namespace ServiceLibrary.Extensions;

public static class LazyServiceExtension
{
    public static void AddLazyService<TService, TImplementation>(
        this IServiceCollection services,
        ServiceLifetime lifetime)
        where TService : class
        where TImplementation : class, TService
    {
        var serviceDescriptor = new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifetime);
        var lazyServiceDescriptor = new ServiceDescriptor(
            typeof(Lazy<TService>),
            provider => new Lazy<TService>(provider.GetRequiredService<TService>),
            lifetime);

        services.Add(serviceDescriptor);
        services.Add(lazyServiceDescriptor);
    }

    public static void AddLazyService<TService, TImplementation>(
        this IServiceCollection services,
        Func<IServiceProvider, TImplementation> implementationFactory,
        ServiceLifetime lifetime)
        where TService : class
        where TImplementation : class, TService
    {
        var serviceDescriptor = new ServiceDescriptor(typeof(TService), implementationFactory, lifetime);
        var lazyServiceDescriptor = new ServiceDescriptor(
            typeof(Lazy<TService>),
            provider => new Lazy<TService>(provider.GetRequiredService<TService>),
            lifetime);

        services.Add(serviceDescriptor);
        services.Add(lazyServiceDescriptor);
    }
}