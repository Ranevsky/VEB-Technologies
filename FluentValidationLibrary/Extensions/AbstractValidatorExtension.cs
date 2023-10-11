using System.Linq.Expressions;
using FluentValidation;

namespace FluentValidationLibrary.Extensions;

public static class AbstractValidatorExtension
{
    public static void NotEmpty<TClass, TCollection>(
        this AbstractValidator<TClass> validator,
        Expression<Func<TClass, TCollection>> expression,
        Action<IRuleBuilder<TClass, TCollection>> rule)
        where TClass : class
        where TCollection : IEnumerable<object>
    {
        validator.When(x =>
                {
                    var collection = expression.Compile().Invoke(x);

                    return ReferenceEquals(collection, null) || !collection.Any();
                },
                () => { validator.RuleFor(expression).NotEmpty(); })
            .Otherwise(() => { rule(validator.RuleFor(expression).NotEmpty()); });
    }

    public static void OptionNotEmpty<TClass, TCollection>(
        this AbstractValidator<TClass> validator,
        Expression<Func<TClass, TCollection?>> expression,
        Action<IRuleBuilder<TClass, TCollection?>> rule)
        where TClass : class
        where TCollection : IEnumerable<object>
    {
        validator.When(x =>
            {
                var collectionGetter = expression.Compile();

                return !ReferenceEquals(collectionGetter(x), null);
            },
            () => { rule(validator.RuleFor(expression).NotEmpty()); });
    }
}