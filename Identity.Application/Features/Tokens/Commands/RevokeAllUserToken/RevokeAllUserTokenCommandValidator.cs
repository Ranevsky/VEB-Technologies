using FluentValidation;
using FluentValidationLibrary.Validators.Id;

namespace Identity.Application.Features.Tokens.Commands.RevokeAllUserToken;

public class RevokeAllUserTokenCommandValidator : AbstractValidator<RevokeAllUserTokenCommand>
{
    public RevokeAllUserTokenCommandValidator(IdValidator idValidator)
    {
        RuleFor(x => x.UserId).SetValidator(idValidator);
    }
}