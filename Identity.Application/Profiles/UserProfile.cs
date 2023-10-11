using AutoMapper;
using Identity.Application.Features.Users.Commands.RegisterUser;
using Identity.Application.Features.Users.Commands.UpdateUser;
using Identity.Application.Features.Users.Queries.GetUserCatalog;
using Identity.Application.Models;
using Identity.Domain.Entities;

namespace Identity.Application.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterUserCommand, UserEntity>()
            .ForMember(command => command.PasswordEntity, opt => opt.Ignore())
            .ForMember(command => command.Roles, opt => opt.MapFrom(user => new List<RoleEntity>()));

        CreateMap<string, EmailEntity>()
            .ConvertUsing(text => new EmailEntity
            {
                Email = text,
            });

        CreateMap<UserEntity, UserClaimModel>()
            .ForMember(userClaimModel => userClaimModel.Email, opt => opt.MapFrom(user => user.Email.Email));

        CreateMap<UserInfo, UserInfoViewModel>();
        CreateMap<UserInfo, UserClaimModel>();

        CreateMap<GetUserCatalogQuery, UserCatalogCondition>();

        CreateMap<UpdateUserCommand, UserUpdateModel>()
            .ForMember(model => model.Name, opt => opt.MapFrom((command, _) => command.Name?.Trim()));
    }
}