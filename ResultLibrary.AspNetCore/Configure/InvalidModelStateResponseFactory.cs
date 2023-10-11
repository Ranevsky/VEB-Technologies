using LoggerLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ResultLibrary.AspNetCore.Results.Error;

namespace ResultLibrary.AspNetCore.Configure;

public static class InvalidModelStateResponseFactory
{
    public static IActionResult DefaultAspValidationResult(ActionContext context)
    {
        var traceIdManager = context.HttpContext.RequestServices.GetRequiredService<ITraceIdManager>();
        var result = new ValidationAspErrorResult(context.ModelState)
        {
            TraceId = traceIdManager.TraceId,
        };

        context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        return new ObjectResult(result);
    }
}