namespace Identity.Application.Models;

public class TokenViewModel
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public DateTime ExpireTime { get; set; }
}