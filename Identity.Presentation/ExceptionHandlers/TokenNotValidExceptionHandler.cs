using ExceptionLibrary.Handlers.ExceptionHandlers;
using Identity.Application.Exceptions;
using Identity.Presentation.ErrorResult;

namespace Identity.Presentation.ExceptionHandlers;

public class TokenNotValidExceptionHandler : ExceptionHandler<TokenNotValidException, TokenValidationErrorResult>
{
    protected override TokenValidationErrorResult Handle(TokenNotValidException exception)
    {
        return new TokenValidationErrorResult();
    }
}