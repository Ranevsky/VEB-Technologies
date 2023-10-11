using ExceptionLibrary.Exceptions;

namespace Identity.Application.Exceptions;

public class EmailConfirmationCodeIncorrectException : IncorrectException
{
    public EmailConfirmationCodeIncorrectException()
        : base("Email confirm code isn't correct.")
    {
    }
}