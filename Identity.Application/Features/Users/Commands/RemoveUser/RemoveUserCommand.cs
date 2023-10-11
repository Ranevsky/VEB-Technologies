using MediatR;

namespace Identity.Application.Features.Users.Commands.RemoveUser;

public class RemoveUserCommand : IRequest<Unit>
{
    public string Id { get; set; } = null!;
}