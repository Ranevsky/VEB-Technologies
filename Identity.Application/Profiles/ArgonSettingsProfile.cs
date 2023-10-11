using AutoMapper;
using Identity.Domain;
using Identity.Domain.Entities;

namespace Identity.Application.Profiles;

public class ArgonSettingsProfile : Profile
{
    public ArgonSettingsProfile()
    {
        CreateMap<DefaultHashSettings, HashSettingsEntity>();
    }
}