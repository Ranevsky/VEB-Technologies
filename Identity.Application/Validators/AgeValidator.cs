using FluentValidation;
using FluentValidation.Validators;
using Identity.Application.Models;

namespace Identity.Application.Validators;

public class AgeValidator<T> : IPropertyValidator<T, int>
{
    private readonly UserRestrictions _userRestrictions;

    public AgeValidator(UserRestrictions userRestrictions)
    {
        _userRestrictions = userRestrictions;
    }

    public bool IsValid(ValidationContext<T> context, int value)
    {
        var isValid = value >= _userRestrictions.MinAge && value <= _userRestrictions.MaxAge;
        if (isValid)
        {
            return isValid;
        }

        var messageFormatter = context.MessageFormatter;
        messageFormatter.AppendArgument("MinAge", _userRestrictions.MinAge);
        messageFormatter.AppendArgument("MaxAge", _userRestrictions.MaxAge);

        return isValid;
    }

    public string GetDefaultMessageTemplate(string errorCode)
    {
        return "Must be between {MinAge} and {MaxAge} age. You entered {PropertyValue} age.";
    }

    public string Name => "AgeRestrictionValidator";
}