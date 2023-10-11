using FluentValidation;
using Identity.Application.Models;

namespace Identity.Application.Features.Users.Queries.GetUserCatalog.Models.Validators;

public class PagingValidator : AbstractValidator<Paging>
{
    public PagingValidator(CatalogRestrictions catalogRestrictions)
    {
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
        RuleFor(x => x.Take).InclusiveBetween(1, catalogRestrictions.MaxItems);
    }
}