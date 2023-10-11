using MediatR;

namespace Identity.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<Unit>
{
    public string Id { get; set; } = null!;
    public string? Name { get; set; }
    public int? Age { get; set; }
}