namespace Identity.Domain.Entities;

public class PasswordEntity
{
    public HashSettingsEntity HashSettings = null!;
    public string Id { get; set; } = null!;
    public string HashPassword { get; set; } = null!;
    public string Salt { get; set; } = null!;

    public string UserId { get; set; } = null!;
}