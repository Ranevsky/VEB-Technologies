using MediatR;

namespace Identity.Application.Features.Users.Commands.RegisterUser;

public class RegisterUserCommand : IRequest<Unit>
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int Age { get; set; }
}