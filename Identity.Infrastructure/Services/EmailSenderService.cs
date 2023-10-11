using Common.Events;
using Identity.Application.Services.Interfaces;
using MassTransit;

namespace Identity.Infrastructure.Services;

internal class EmailSenderService : IEmailSenderService
{
    private readonly IPublishEndpoint _publisher;

    public EmailSenderService(IPublishEndpoint publisher)
    {
        _publisher = publisher;
    }

    public async Task SendConfirmCodeAsync(string email, string emailId, string confirmationKey)
    {
        await _publisher.Publish(new SentEmailConfirmationEvent
        {
            Email = email,
            EmailId = emailId,
            ConfirmationKey = confirmationKey,
        });
    }
}