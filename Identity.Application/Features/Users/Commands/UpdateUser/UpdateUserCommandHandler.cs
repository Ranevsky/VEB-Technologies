using AutoMapper;
using Identity.Application.Models;
using Identity.Application.Repositories.Interfaces;
using MediatR;

namespace Identity.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(
        IUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        var updateModel = _mapper.Map<UserUpdateModel>(request);
        await _userRepository.UpdateAsync(request.Id, updateModel, cancellationToken);

        return Unit.Value;
    }
}