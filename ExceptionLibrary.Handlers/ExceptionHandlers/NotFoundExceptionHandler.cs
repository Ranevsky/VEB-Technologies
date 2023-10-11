using ExceptionLibrary.Exceptions;
using ResultLibrary.Results.Error;

namespace ExceptionLibrary.Handlers.ExceptionHandlers;

public class NotFoundExceptionHandler : ExceptionHandler<NotFoundException, NotFoundErrorResult>
{
    protected override NotFoundErrorResult Handle(NotFoundException exception)
    {
        return new NotFoundErrorResult(exception.Message);
    }
}