namespace Identity.Domain.Entities;

public class TokenEntity
{
    public string Id { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public DateTime ExpireTime { get; set; }

    public UserEntity UserEntity { get; set; } = null!;
}