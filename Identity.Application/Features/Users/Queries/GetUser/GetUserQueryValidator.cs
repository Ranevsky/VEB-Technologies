using FluentValidation;
using FluentValidationLibrary.Validators.Id;

namespace Identity.Application.Features.Users.Queries.GetUser;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator(IdValidator idValidator)
    {
        RuleFor(x => x.Id).SetValidator(idValidator);
    }
}