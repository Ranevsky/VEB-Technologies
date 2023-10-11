using FluentValidation;
using FluentValidationLibrary.Validators.Id;

namespace Identity.Application.Features.Users.Commands.AddRoleToUser;

public class AddRoleToUserCommandValidator : AbstractValidator<AddRoleToUserCommand>
{
    public AddRoleToUserCommandValidator(IdValidator idValidator)
    {
        RuleFor(x => x.UserId).SetValidator(idValidator);
        RuleFor(x => x.RoleId).SetValidator(idValidator);
    }
}