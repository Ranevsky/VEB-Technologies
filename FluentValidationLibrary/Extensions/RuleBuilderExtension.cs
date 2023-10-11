using FluentValidation;

namespace FluentValidationLibrary.Extensions;

public static class RuleBuilderExtension
{
    public static IRuleBuilderOptions<T, string?> NotEmptyWithoutPropertyName<T>(
        this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage("Must not be empty.");
    }

    public static IRuleBuilderOptions<T, string?> LengthWithoutPropertyName<T>(
        this IRuleBuilder<T, string?> ruleBuilder,
        int exactLength)
    {
        return ruleBuilder
            .Length(exactLength)
            .WithMessage("Must be {MaxLength} characters in length. You entered {TotalLength} characters.");
    }

    public static IRuleBuilderOptions<TEntityValidation, TCollection> Unique<TEntityValidation, TCollection, TElement>(
        this IRuleBuilder<TEntityValidation, TCollection> ruleBuilder)
        where TCollection : IEnumerable<TElement>
    {
        return ruleBuilder.SetValidator(new UniqueValidator<TEntityValidation, TCollection, TElement>());
    }
}