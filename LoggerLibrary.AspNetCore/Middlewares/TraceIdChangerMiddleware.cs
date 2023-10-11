using LoggerLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LoggerLibrary.AspNetCore.Middlewares;

public class TraceIdChangerMiddleware
{
    private readonly RequestDelegate _next;

    public TraceIdChangerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var traceIdManager = context.RequestServices.GetRequiredService<ITraceIdManager>();

        if (context.Request.Headers.TryGetValue(HeadersConst.TraceId, out var id))
        {
            traceIdManager.TraceId = id;
        }

        context.TraceIdentifier = traceIdManager.TraceId;

        await _next.Invoke(context);
    }
}