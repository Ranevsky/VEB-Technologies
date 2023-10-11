namespace Identity.Infrastructure;

internal class RefreshTokenSettings
{
    public int TokenLength { get; set; }
    public int ExpireTime { get; set; }
}