using AutoMapper;
using Identity.Application.Repositories.Interfaces;
using MediatR;

namespace Identity.Application.Features.Role.Commands.CreateRole;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, string>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public CreateRoleCommandHandler(
        IRoleRepository roleRepository,
        IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<string> Handle(
        CreateRoleCommand request,
        CancellationToken cancellationToken)
    {
        var role = _mapper.Map<Domain.Entities.RoleEntity>(request);

        var id = await _roleRepository.CreateAsync(role, cancellationToken);

        return id;
    }
}