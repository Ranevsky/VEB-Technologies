using LoggerLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LoggerLibrary.AspNetCore.Middlewares;

public class TraceIdLogMiddleware
{
    private readonly ILogger<TraceIdLogMiddleware> _logger;
    private readonly RequestDelegate _next;

    public TraceIdLogMiddleware(
        RequestDelegate next,
        ILogger<TraceIdLogMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var traceIdManager = context.RequestServices.GetRequiredService<ITraceIdManager>();

        using var _ = _logger.BeginScope(traceIdManager.TraceId);
        await _next.Invoke(context);
    }
}