using System.Net;

namespace ResultLibrary.Results.Error;

public class UnhandledExceptionErrorResult : ServerErrorResult
{
    public UnhandledExceptionErrorResult()
        : base("Internal exception.", (int)HttpStatusCode.InternalServerError)
    {
        Message = "Exception not handled.";
    }
}