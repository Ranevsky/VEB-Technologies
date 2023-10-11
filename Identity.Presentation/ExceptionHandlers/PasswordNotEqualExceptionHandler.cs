using ExceptionLibrary.Handlers.ExceptionHandlers;
using Identity.Application.Exceptions;
using Identity.Presentation.ErrorResult;

namespace Identity.Presentation.ExceptionHandlers;

public class PasswordNotEqualExceptionHandler : ExceptionHandler<PasswordNotEqualException, PasswordNotEqualErrorResult>
{
    protected override PasswordNotEqualErrorResult Handle(PasswordNotEqualException exception)
    {
        return new PasswordNotEqualErrorResult();
    }
}