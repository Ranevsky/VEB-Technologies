using ExceptionLibrary.Handlers.AspNetCore.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace ExceptionLibrary.Handlers.AspNetCore.Extensions;

public static class ExceptionHandlerMiddlewareExtension
{
    public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}