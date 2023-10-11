using System.Net;

namespace Identity.Presentation.ErrorResult;

public class UserExistByEmailErrorResult : ResultLibrary.Results.Error.ErrorResult
{
    public UserExistByEmailErrorResult()
        : base("User already exists.", (int)HttpStatusCode.BadRequest)
    {
    }
}