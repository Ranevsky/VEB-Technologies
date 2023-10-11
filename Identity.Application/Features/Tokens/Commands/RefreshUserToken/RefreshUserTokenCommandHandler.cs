using AutoMapper;
using Identity.Application.Exceptions;
using Identity.Application.Models;
using Identity.Application.Repositories.Interfaces;
using Identity.Application.Services.Interfaces;
using Identity.Domain.Entities;
using MediatR;

namespace Identity.Application.Features.Tokens.Commands.RefreshUserToken;

public class RefreshUserTokenCommandHandler : IRequestHandler<UserRefreshTokenCommand, TokenViewModel>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ITokenService<UserClaimModel> _tokenService;

    public RefreshUserTokenCommandHandler(
        ITokenService<UserClaimModel> tokenService,
        IRefreshTokenRepository refreshTokenRepository,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _tokenService = tokenService;
        _refreshTokenRepository = refreshTokenRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<TokenViewModel> Handle(
        UserRefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var userClaimModel = _tokenService.GetClaimModel(request.AccessToken);

        TokenEntity tokenEntity;
        try
        {
            tokenEntity = await _refreshTokenRepository.GetAsync(userClaimModel.Id, request.RefreshToken, cancellationToken);
        }
        catch (TokenNotValidException)
        {
            throw new TokenNotValidException();
        }

        if (tokenEntity.ExpireTime <= DateTime.UtcNow)
        {
            await _refreshTokenRepository.RemoveAsync(userClaimModel.Id, tokenEntity.Id, cancellationToken);

            throw new TokenNotValidException();
        }

        var userInfo = await _userRepository.GetUserInfoByIdAsync(userClaimModel.Id, cancellationToken);
        var newUserClaimModel = _mapper.Map<UserClaimModel>(userInfo);
        
        var newAccessToken = _tokenService.CreateAccessToken(newUserClaimModel);
        var newRefreshToken = _tokenService.CreateRefreshToken();

        await _refreshTokenRepository.UpdateAsync(
            tokenEntity.Id,
            newRefreshToken.Token,
            newRefreshToken.ExpireTime,
            cancellationToken);

        var result = new TokenViewModel
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken.Token,
            ExpireTime = newRefreshToken.ExpireTime,
        };

        return result;
    }
}