using FluentValidation;
using FluentValidationLibrary.Validators.Id;

namespace Identity.Application.Features.Tokens.Commands.RevokeUserToken;

public class RevokeUserTokenCommandValidator : AbstractValidator<RevokeUserTokenCommand>
{
    public RevokeUserTokenCommandValidator(IdValidator idValidator)
    {
        RuleFor(x => x.UserId).SetValidator(idValidator);
        RuleFor(x => x.TokenId).SetValidator(idValidator);
    }
}