using Identity.Application.Repositories.Interfaces;
using MediatR;

namespace Identity.Application.Features.Users.Commands.RemoveUser;

public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand, Unit>
{
    private readonly IUserRepository _userRepository;

    public RemoveUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(
        RemoveUserCommand request,
        CancellationToken cancellationToken)
    {
        await _userRepository.RemoveByIdAsync(request.Id, cancellationToken);

        return Unit.Value;
    }
}