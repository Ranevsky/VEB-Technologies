using ExceptionLibrary.Exceptions;
using ResultLibrary.Results.Error;

namespace ExceptionLibrary.Handlers.ExceptionHandlers;

public class IncorrectExceptionHandler : ExceptionHandler<IncorrectException, IncorrectErrorResult>
{
    protected override IncorrectErrorResult Handle(IncorrectException exception)
    {
        return new IncorrectErrorResult(exception.Details);
    }
}