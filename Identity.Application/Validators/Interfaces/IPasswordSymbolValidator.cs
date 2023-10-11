using FluentValidation.Validators;

namespace Identity.Application.Validators.Interfaces;

public interface IPasswordSymbolValidator<T> : IPropertyValidator<T, string>
{
}