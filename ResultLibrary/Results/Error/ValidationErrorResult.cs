using System.Net;

namespace ResultLibrary.Results.Error;

public class ValidationErrorResult : ErrorResult
{
    public ValidationErrorResult(IDictionary<string, string[]> errors)
        : base("One or many validation error.", (int)HttpStatusCode.BadRequest)
    {
        Errors = errors;
    }

    public IDictionary<string, string[]> Errors { get; set; }
}