using AutoMapper;
using Identity.Application.Models;
using Identity.Application.Repositories.Interfaces;
using MediatR;

namespace Identity.Application.Features.Users.Queries.GetUserCatalog;

public class GetUserCatalogQueryHandler : IRequestHandler<GetUserCatalogQuery, UserCatalogViewModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserCatalogQueryHandler(
        IUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserCatalogViewModel> Handle(
        GetUserCatalogQuery request,
        CancellationToken cancellationToken)
    {
        var condition = _mapper.Map<UserCatalogCondition>(request);
        var catalog = await _userRepository.GetUserCatalogAsync(condition, cancellationToken);

        return catalog;
    }
}