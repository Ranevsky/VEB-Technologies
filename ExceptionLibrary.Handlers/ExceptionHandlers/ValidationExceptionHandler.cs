using ExceptionLibrary.Exceptions;
using ResultLibrary.Results.Error;

namespace ExceptionLibrary.Handlers.ExceptionHandlers;

public class ValidationExceptionHandler : ExceptionHandler<ValidationException, ValidationErrorResult>
{
    protected override ValidationErrorResult Handle(ValidationException exception)
    {
        return new ValidationErrorResult(exception.Errors);
    }
}