using AutoMapper;
using Identity.Application.Features.Role.Commands.CreateRole;
using Identity.Domain.Entities;

namespace Identity.Application.Profiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<RoleEntity, string>()
            .ConvertUsing(role => role.Name);

        CreateMap<CreateRoleCommand, RoleEntity>();
    }
}