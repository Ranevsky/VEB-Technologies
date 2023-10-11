using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ResultLibrary.AspNetCore.Results.Action;

public class CreatedObjectResult : ObjectResult
{
    public CreatedObjectResult(object? value)
        : base(value)
    {
    }

    public override Task ExecuteResultAsync(ActionContext context)
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status201Created;

        return base.ExecuteResultAsync(context);
    }
}