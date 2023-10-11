using System.Net;

namespace ResultLibrary.Results.Error;

public class NotFoundErrorResult : ErrorResult
{
    public NotFoundErrorResult(string detail)
        : base("Not Found.", (int)HttpStatusCode.NotFound)
    {
        Detail = detail;
    }

    public string Detail { get; set; }
}