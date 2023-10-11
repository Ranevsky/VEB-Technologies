using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;

namespace FluentValidationLibrary;

public class UniqueValidator<T, TCollection, TItem> : IPropertyValidator<T, TCollection>
    where TCollection : IEnumerable<TItem>
{
    public bool IsValid(ValidationContext<T> context, TCollection value)
    {
        var result = true;
        var name = context.PropertyName;
        var set = new HashSet<TItem>();
        var index = 0;
        foreach (var item in value)
        {
            if (set.TryGetValue(item, out _))
            {
                context.AddFailure(new ValidationFailure($"{name}[{index}]", "The value is repeated."));
                result = false;
            }

            set.Add(item);
            index++;
        }

        return result;
    }

    public string GetDefaultMessageTemplate(string errorCode)
    {
        return "Should contain only unique elements.";
    }

    public string Name => "UniqueElementValidator";
}