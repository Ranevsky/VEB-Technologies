using ExceptionLibrary.Exceptions;
using ResultLibrary.Results.Error;

namespace ExceptionLibrary.Handlers.ExceptionHandlers;

public class ModelExistExceptionHandler : ExceptionHandler<ModelExistException, ModelExistErrorResult>
{
    protected override ModelExistErrorResult Handle(ModelExistException exception)
    {
        return new ModelExistErrorResult(exception.Message);
    }
}