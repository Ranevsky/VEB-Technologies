using System.Net;

namespace ResultLibrary.Results.Error;

public class IncorrectErrorResult : ErrorResult
{
    public IncorrectErrorResult(string details)
        : base("Incorrect data.", (int)HttpStatusCode.BadRequest)
    {
        Details = details;
    }

    public string Details { get; set; }
}