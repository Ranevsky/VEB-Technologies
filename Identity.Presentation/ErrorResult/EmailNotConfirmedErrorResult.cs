using System.Net;

namespace Identity.Presentation.ErrorResult;

public class EmailNotConfirmedErrorResult : ResultLibrary.Results.Error.ErrorResult
{
    public EmailNotConfirmedErrorResult()
        : base("Email not confirmed.", (int)HttpStatusCode.Forbidden)
    {
    }
}