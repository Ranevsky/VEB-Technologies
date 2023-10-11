namespace Identity.Domain.Entities;

public class EmailEntity
{
    public string Id { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? ConfirmCode { get; set; }
    public string UserId { get; set; } = null!;
}