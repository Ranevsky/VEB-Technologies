namespace Common.Events;

public class SentEmailConfirmationEvent
{
    public string Email { get; set; } = null!;
    public string EmailId { get; set; } = null!;
    public string ConfirmationKey { get; set; } = null!;
}