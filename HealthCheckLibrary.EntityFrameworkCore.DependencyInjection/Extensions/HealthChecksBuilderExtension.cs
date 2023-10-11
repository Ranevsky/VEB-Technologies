using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HealthCheckLibrary.EntityFrameworkCore.DependencyInjection.Extensions;

public static class HealthChecksBuilderExtension
{
    public static IHealthChecksBuilder AddDbContextCheck<TDbContext>(this IHealthChecksBuilder builder, string name)
        where TDbContext : DbContext
    {
        return builder.AddDbContextCheck<TDbContext>(name, tags: new[] { HealthCheckTags.Db });
    }
}