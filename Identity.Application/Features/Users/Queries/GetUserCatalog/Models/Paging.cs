namespace Identity.Application.Features.Users.Queries.GetUserCatalog.Models;

public class Paging
{
    public int Take { get; set; } = 1;
    public int Page { get; set; } = 1;
}