using System.Net;

namespace Identity.Presentation.ErrorResult;

public class PasswordNotEqualErrorResult : ResultLibrary.Results.Error.ErrorResult
{
    public PasswordNotEqualErrorResult()
        : base("Password doesn't match.", (int)HttpStatusCode.BadRequest)
    {
    }
}