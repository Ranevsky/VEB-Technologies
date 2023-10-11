using FluentValidation;
using Identity.Application.Features.Users.Queries.GetUserCatalog.Models.Validators;

namespace Identity.Application.Features.Users.Queries.GetUserCatalog;

public class GetUserCatalogQueryValidator : AbstractValidator<GetUserCatalogQuery>
{
    public GetUserCatalogQueryValidator(
        UserCatalogFilterValidator filterValidator,
        PagingValidator pagingValidator,
        UserCatalogSortValidator sortValidator)
    {
        RuleFor(x => x.Filters).SetValidator(filterValidator!);
        RuleFor(x => x.Paging).SetValidator(pagingValidator);
        RuleFor(x => x.Sort).SetValidator(sortValidator!);
    }
}