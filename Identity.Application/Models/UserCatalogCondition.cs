using Identity.Application.Features.Users.Queries.GetUserCatalog.Models;

namespace Identity.Application.Models;

public class UserCatalogCondition
{
    public Paging Paging { get; set; } = null!;
    public UserCatalogFilter? Filters { get; set; }
    public UserCatalogSort? Sort { get; set; }
}