using AutoMapper;
using Identity.Application.Exceptions;
using Identity.Application.Models;
using Identity.Application.Repositories.Interfaces;
using Identity.Application.Services.Interfaces;
using Identity.Domain.Entities;
using MediatR;

namespace Identity.Application.Features.Users.Queries.LoginUser;

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, TokenViewModel>
{
    private readonly IMapper _mapper;
    private readonly IPasswordManager _passwordManager;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenService<UserClaimModel> _tokenService;
    private readonly IUserRepository _userRepository;

    public LoginUserQueryHandler(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IPasswordManager passwordManager,
        ITokenService<UserClaimModel> tokenService,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _passwordManager = passwordManager;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    public async Task<TokenViewModel> Handle(
        LoginUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailWithPasswordAsync(request.Email, cancellationToken);

        var isEquals = await _passwordManager.IsEqualsAsync(user.PasswordEntity, request.Password, cancellationToken);
        if (!isEquals)
        {
            throw new PasswordNotEqualException();
        }

        if (user.Email.ConfirmCode is not null)
        {
            throw new EmailNotConfirmedException();
        }

        var refreshToken = _tokenService.CreateRefreshToken();

        var token = _mapper.Map<TokenEntity>(refreshToken);
        await _refreshTokenRepository.AddAsync(user.Id, token, cancellationToken);

        var claimModel = _mapper.Map<UserClaimModel>(user);
        var accessToken = _tokenService.CreateAccessToken(claimModel);

        var result = new TokenViewModel
        {
            AccessToken = accessToken,
            RefreshToken = token.RefreshToken,
            ExpireTime = token.ExpireTime,
        };

        return result;
    }
}