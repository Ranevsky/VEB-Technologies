using Identity.Application.Models;
using MediatR;

namespace Identity.Application.Features.Role.Queries.GetAllRoles;

public class GetAllRolesQuery : IRequest<ICollection<RoleInfoView>>
{
}