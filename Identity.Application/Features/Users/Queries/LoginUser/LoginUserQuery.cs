using Identity.Application.Models;
using MediatR;

namespace Identity.Application.Features.Users.Queries.LoginUser;

public class LoginUserQuery : IRequest<TokenViewModel>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}