using System.Net;

namespace ResultLibrary.Results.Error;

public class ClaimNotFoundErrorResult : ServerErrorResult
{
    public ClaimNotFoundErrorResult(string? message = null)
        : base(null, (int)HttpStatusCode.InternalServerError)
    {
        Message = message;
    }
}