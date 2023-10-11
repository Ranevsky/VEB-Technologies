using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Serilog;

namespace LoggerLibrary.Serilog.Extensions;

public static class SerilogExtension
{
    public static void UseSerilog(this WebApplicationBuilder builder)
    {
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        var loggingBuilder = builder.Logging;
        loggingBuilder.ClearProviders();
        loggingBuilder.AddSerilog(logger);
    }
}