using Identity.Application.Models;
using ResultLibrary.Models.CatalogInfo;

namespace Identity.Application.Features.Users.Queries.GetUserCatalog;

public class UserCatalogViewModel : CatalogInfo
{
    public ICollection<UserInfo>? Users { get; set; }
}