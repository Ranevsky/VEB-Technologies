using AutoMapper;
using Identity.Application.Repositories.Interfaces;
using Identity.Application.Services.Interfaces;
using Identity.Domain.Entities;
using MediatR;

namespace Identity.Application.Features.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Unit>
{
    private readonly IEmailSenderService _emailSenderService;
    private readonly IMapper _mapper;
    private readonly IPasswordManager _passwordManager;
    private readonly IRandomGeneratorService _randomGeneratorService;
    private readonly IUserRepository _userRepository;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IPasswordManager passwordManager,
        IEmailSenderService emailSenderService,
        IRandomGeneratorService randomGeneratorService,
        IMapper mapper)
    {
        _mapper = mapper;
        _passwordManager = passwordManager;
        _emailSenderService = emailSenderService;
        _randomGeneratorService = randomGeneratorService;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = _mapper.Map<UserEntity>(request);

        var password = await _passwordManager.CreatePasswordAsync(request.Password, cancellationToken);
        user.PasswordEntity = password;

        var confirmKey = _randomGeneratorService.Generate(15);
        user.Email.ConfirmCode = confirmKey;

        await _userRepository.CreateAsync(user, cancellationToken);
        await _emailSenderService.SendConfirmCodeAsync(user.Email.Email, user.Email.Id, confirmKey);

        return Unit.Value;
    }
}