using System.Net.Mime;
using System.Text.Json;
using ExceptionLibrary.Handlers.ExceptionHandlers;
using LoggerLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ResultLibrary.Results.Error;

namespace ExceptionLibrary.Handlers.AspNetCore.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly IDictionary<Type, ExceptionHandler> _handlers;
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(
        RequestDelegate next,
        IDictionary<Type, ExceptionHandler> handlers)
    {
        _next = next;
        _handlers = handlers;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            object? result = null;
            var exceptionType = ex.GetType();
            while (exceptionType is not null)
            {
                if (!_handlers.TryGetValue(exceptionType, out var handler))
                {
                    exceptionType = exceptionType.BaseType;

                    continue;
                }

                result = handler.Handle(ex);

                break;
            }

            if (exceptionType is null)
            {
                throw;
            }

            if (result is not ErrorResult errorResult)
            {
                return;
            }

            if (result is ServerErrorResult serverErrorResult)
            {
                var logger = context.RequestServices.GetRequiredService<ILogger<ExceptionHandlerMiddleware>>();
                logger.LogError(ex, "{Message}", serverErrorResult.Message);

                errorResult = new ErrorResult(serverErrorResult.Title, serverErrorResult.StatusCode);
            }

            var response = context.Response;
            var traceIdManager = context.RequestServices.GetRequiredService<ITraceIdManager>();
            errorResult.TraceId = traceIdManager.TraceId;
            response.StatusCode = errorResult.StatusCode;

            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize((object)errorResult, jsonOptions);
            response.ContentType = MediaTypeNames.Application.Json;
            await response.WriteAsync(json);
        }
    }
}