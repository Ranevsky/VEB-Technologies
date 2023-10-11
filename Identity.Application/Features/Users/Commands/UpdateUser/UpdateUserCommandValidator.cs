using FluentValidation;
using FluentValidationLibrary.Validators.Id;
using Identity.Application.Validators;

namespace Identity.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator(
        IdValidator idValidator,
        AgeValidator<UpdateUserCommand> ageValidator)
    {
        RuleFor(x => x.Id).SetValidator(idValidator);
        RuleFor(x => x.Age).SetValidator(ageValidator);
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .When(x => x.Name is not null);
    }
}