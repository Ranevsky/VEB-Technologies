using Identity.Application.Repositories.Interfaces;
using MediatR;

namespace Identity.Application.Features.Users.Commands.AddRoleToUser;

public class AddRoleToUserCommandHandler : IRequestHandler<AddRoleToUserCommand, Unit>
{
    private readonly IUserRepository _userRepository;

    public AddRoleToUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(
        AddRoleToUserCommand request,
        CancellationToken cancellationToken)
    {
        await _userRepository.AddRoleAsync(request.UserId, request.RoleId, cancellationToken);
        
        return Unit.Value;
    }
}