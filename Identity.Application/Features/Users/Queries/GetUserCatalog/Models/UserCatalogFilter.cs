namespace Identity.Application.Features.Users.Queries.GetUserCatalog.Models;

public class UserCatalogFilter
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public AgeBetween? Age { get; set; }
    public ICollection<string>? Roles { get; set; }
}