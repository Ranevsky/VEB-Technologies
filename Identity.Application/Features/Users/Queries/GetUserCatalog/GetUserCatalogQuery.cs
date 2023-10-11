using Identity.Application.Features.Users.Queries.GetUserCatalog.Models;
using MediatR;

namespace Identity.Application.Features.Users.Queries.GetUserCatalog;

public class GetUserCatalogQuery : IRequest<UserCatalogViewModel>
{
    public Paging Paging { get; set; } = null!;
    public UserCatalogFilter? Filters { get; set; }
    public UserCatalogSort? Sort { get; set; }
}

