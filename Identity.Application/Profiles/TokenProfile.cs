using AutoMapper;
using Identity.Domain.Entities;

namespace Identity.Application.Profiles;

public class TokenProfile : Profile
{
    public TokenProfile()
    {
        CreateMap<RefreshToken, TokenEntity>()
            .ForMember(token => token.RefreshToken, opt => opt.MapFrom(refreshToken => refreshToken.Token))
            .ForMember(token => token.ExpireTime, opt => opt.MapFrom(refreshToken => refreshToken.ExpireTime));
    }
}