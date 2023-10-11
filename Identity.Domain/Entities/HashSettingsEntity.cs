namespace Identity.Domain.Entities;

public class HashSettingsEntity : HashSettings
{
    public string Id { get; set; } = null!;
    public ICollection<PasswordEntity>? Passwords { get; set; } = null!;
}