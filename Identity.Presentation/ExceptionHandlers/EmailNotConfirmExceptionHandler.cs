using ExceptionLibrary.Handlers.ExceptionHandlers;
using Identity.Application.Exceptions;
using Identity.Presentation.ErrorResult;

namespace Identity.Presentation.ExceptionHandlers;

public class
    EmailNotConfirmExceptionHandler : ExceptionHandler<EmailNotConfirmedException, EmailNotConfirmedErrorResult>
{
    protected override EmailNotConfirmedErrorResult Handle(EmailNotConfirmedException exception)
    {
        return new EmailNotConfirmedErrorResult();
    }
}