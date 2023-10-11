namespace Identity.Application;

public class TokenSettings
{
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int ExpireTime { get; set; }
    public string Key { get; set; } = null!;
    public string RefreshTokenLength { get; set; } = null!;
}