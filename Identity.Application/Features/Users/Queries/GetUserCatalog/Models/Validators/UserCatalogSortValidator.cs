using FluentValidation;

namespace Identity.Application.Features.Users.Queries.GetUserCatalog.Models.Validators;

public class UserCatalogSortValidator : AbstractValidator<UserCatalogSort>
{
    public UserCatalogSortValidator()
    {
        RuleFor(x => x.SortField).IsInEnum();
    }
}