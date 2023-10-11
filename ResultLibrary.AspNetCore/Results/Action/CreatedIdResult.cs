using ResultLibrary.Models;

namespace ResultLibrary.AspNetCore.Results.Action;

public class CreatedIdResult<TId> : CreatedObjectResult
{
    public CreatedIdResult(TId value)
        : base(new IdViewModel<TId>(value))
    {
    }
}