using ExceptionLibrary.Exceptions;
using ResultLibrary.Results.Error;

namespace ExceptionLibrary.Handlers.ExceptionHandlers;

public class EmptyUpdateExceptionHandler : ExceptionHandler<EmptyUpdateException, EmptyUpdateErrorResult>
{
    protected override EmptyUpdateErrorResult Handle(EmptyUpdateException exception)
    {
        return new EmptyUpdateErrorResult(exception.Message);
    }
}