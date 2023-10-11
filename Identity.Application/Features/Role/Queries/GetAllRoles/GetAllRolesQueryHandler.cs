using Identity.Application.Models;
using Identity.Application.Repositories.Interfaces;
using MediatR;

namespace Identity.Application.Features.Role.Queries.GetAllRoles;

public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, ICollection<RoleInfoView>>
{
    private readonly IRoleRepository _roleRepository;

    public GetAllRolesQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<ICollection<RoleInfoView>> Handle(
        GetAllRolesQuery request,
        CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.GetAll(cancellationToken);

        return roles;
    }
}