using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceLibrary.Extensions;

public static class FromConfigurationServiceExtension
{
    public static IServiceCollection AddFromConfiguration<TService>(
        this IServiceCollection services,
        string configurationKey,
        ServiceLifetime lifetime)
        where TService : class
    {
        var descriptor = new ServiceDescriptor(
            typeof(TService),
            service => service.GetRequiredService<IConfiguration>()
                .GetRequiredSection(configurationKey)
                .Get<TService>(),
            lifetime);

        services.Add(descriptor);

        return services;
    }
}