using AutoMapper;
using Identity.Application.Models;
using Identity.Application.Repositories.Interfaces;
using MediatR;

namespace Identity.Application.Features.Users.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserInfoViewModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(
        IUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserInfoViewModel> Handle(
        GetUserQuery request,
        CancellationToken cancellationToken)
    {
        var userInfo = await _userRepository.GetUserInfoByIdAsync(request.Id, cancellationToken);
        var result = _mapper.Map<UserInfoViewModel>(userInfo);

        return result;
    }
}