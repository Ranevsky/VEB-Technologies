namespace Identity.Domain;

public class DefaultHashSettings : HashSettings
{
    public int SaltLength { get; set; }
}