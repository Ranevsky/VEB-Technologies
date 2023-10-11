using FluentValidation;
using Identity.Application.Models;

namespace Identity.Application.Features.Users.Queries.GetUserCatalog.Models.Validators;

public class AgeBetweenValidator : AbstractValidator<AgeBetween>
{
    public AgeBetweenValidator(UserRestrictions userRestrictions)
    {
        RuleFor(x => x.From).InclusiveBetween(userRestrictions.MinAge, userRestrictions.MaxAge);
        RuleFor(x => x.To).InclusiveBetween(userRestrictions.MinAge, userRestrictions.MaxAge);
        
        RuleFor(x => x)
            .Must(x => x.To >= x.From)
            .WithMessage($"'{nameof(AgeBetween.To)}' must be greater than or equal to '{nameof(AgeBetween.From)}'")
            .WithName(nameof(AgeBetween));
    }
}