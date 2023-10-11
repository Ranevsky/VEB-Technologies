using AutoMapper;
using Identity.Application.Features.Passwords.Commands.ChangeUserPassword;
using Identity.Application.Features.Tokens.Commands.RevokeUserToken;
using Identity.Application.Features.Users.Commands.UpdateUser;
using Identity.Presentation.Models;

namespace Identity.Presentation.Profiles;

public class DataModelProfile : Profile
{
    public DataModelProfile()
    {
        CreateMap<ChangeUserPasswordDataModel, ChangeUserPasswordCommand>();
        CreateMap<RevokeUserTokenDataModel, RevokeUserTokenCommand>();
        CreateMap<UpdateUserDataModel, UpdateUserCommand>();
    }
}