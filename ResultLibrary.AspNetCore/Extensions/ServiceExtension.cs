using LoggerLibrary.Services;
using LoggerLibrary.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ResultLibrary.AspNetCore.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddTraceIdService(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddScoped<ITraceIdManager, GuidTraceIdManager>();
    }
}