using MediatR;

namespace Identity.Application.Features.Users.Commands.AddRoleToUser;

public class AddRoleToUserCommand : IRequest<Unit>
{
    public string UserId { get; set; } = null!;
    public string RoleId { get; set; } = null!;
}