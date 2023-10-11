using ExceptionLibrary.Handlers.ExceptionHandlers;
using Identity.Application.Exceptions;
using Identity.Presentation.ErrorResult;

namespace Identity.Presentation.ExceptionHandlers;

public class UserExistByEmailExceptionHandler : ExceptionHandler<UserModelExistByEmailException, UserExistByEmailErrorResult>
{
    protected override UserExistByEmailErrorResult Handle(UserModelExistByEmailException byEmailException)
    {
        return new UserExistByEmailErrorResult();
    }
}