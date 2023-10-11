using FluentValidation;
using FluentValidationLibrary.Extensions;

namespace Identity.Application.Features.Users.Queries.GetUserCatalog.Models.Validators;

public class UserCatalogFilterValidator : AbstractValidator<UserCatalogFilter>
{
    public UserCatalogFilterValidator(AgeBetweenValidator ageBetweenValidator)
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Age).SetValidator(ageBetweenValidator!);
        RuleFor(x => x.Name).NotEmpty();
        this.OptionNotEmpty(x => x.Roles, rule => { rule.ForEach(x => x.NotEmpty()); });
    }
}