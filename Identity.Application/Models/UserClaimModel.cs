namespace Identity.Application.Models;

public class UserClaimModel : ClaimModel
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public ICollection<string> Roles { get; set; } = null!;
}