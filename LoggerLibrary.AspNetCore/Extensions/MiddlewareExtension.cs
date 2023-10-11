using LoggerLibrary.AspNetCore.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace LoggerLibrary.AspNetCore.Extensions;

public static class MiddlewareExtension
{
    public static void UseTraceIdLog(this IApplicationBuilder app)
    {
        app.UseMiddleware<TraceIdChangerMiddleware>();
        app.UseMiddleware<TraceIdLogMiddleware>();
    }
}