using Identity.Application.Models;
using MediatR;

namespace Identity.Application.Features.Users.Queries.GetUser;

public class GetUserQuery : IRequest<UserInfoViewModel>
{
    public string Id { get; set; } = null!;
}