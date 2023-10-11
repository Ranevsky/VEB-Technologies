using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;

namespace HealthCheckLibrary.AspNetCore.Routing.Extensions;

public static class EndpointRouteBuilderExtension
{
    private const string StartPath = "/health";
    private const string AllPath = StartPath + "/all";
    private const string DbPath = StartPath + "/" + HealthCheckTags.Db;

    public static IEndpointConventionBuilder AddAllMapHealthChecks(this IEndpointRouteBuilder builder)
    {
        return builder.MapHealthChecks(AllPath, new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        });
    }

    public static IEndpointConventionBuilder AddDbMapHealthChecks(this IEndpointRouteBuilder builder)
    {
        return builder.MapHealthChecks(DbPath, new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        });
    }
}