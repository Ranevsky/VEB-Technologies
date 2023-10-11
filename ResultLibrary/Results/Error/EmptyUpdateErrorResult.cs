using System.Net;

namespace ResultLibrary.Results.Error;

public class EmptyUpdateErrorResult : ErrorResult
{
    public EmptyUpdateErrorResult(string detail)
        : base("Empty update.", (int)HttpStatusCode.BadRequest)
    {
        Detail = detail;
    }

    public string Detail { get; set; }
}