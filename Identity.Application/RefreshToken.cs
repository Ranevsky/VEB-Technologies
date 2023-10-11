namespace Identity.Application;

public class RefreshToken
{
    public string Token { get; set; } = null!;
    public DateTime ExpireTime { get; set; }
}