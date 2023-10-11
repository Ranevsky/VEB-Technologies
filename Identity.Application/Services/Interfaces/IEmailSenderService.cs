namespace Identity.Application.Services.Interfaces;

public interface IEmailSenderService
{
    Task SendConfirmCodeAsync(string email, string emailId, string confirmationKey);
}