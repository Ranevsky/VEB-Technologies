using System.Net;

namespace Identity.Presentation.ErrorResult;

public class TokenValidationErrorResult : ResultLibrary.Results.Error.ErrorResult
{
    public TokenValidationErrorResult(string? details = null)
        : base("Token invalid.", (int)HttpStatusCode.Unauthorized)
    {
        Details = details;
    }

    public string? Details { get; set; }
}