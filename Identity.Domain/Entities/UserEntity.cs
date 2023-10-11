namespace Identity.Domain.Entities;

public class UserEntity
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public EmailEntity Email { get; set; } = null!;

    public PasswordEntity PasswordEntity { get; set; } = null!;
    public ICollection<TokenEntity> Tokens { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int Age { get; set; }
    public ICollection<RoleEntity> Roles { get; set; } = null!;
}