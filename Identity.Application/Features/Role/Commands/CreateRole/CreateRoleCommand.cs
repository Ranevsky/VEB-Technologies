using MediatR;

namespace Identity.Application.Features.Role.Commands.CreateRole;

public class CreateRoleCommand : IRequest<string>
{
    public string Name { get; set; } = null!;
}