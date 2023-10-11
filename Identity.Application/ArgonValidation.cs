using FluentValidation;
using Identity.Domain;

namespace Identity.Application;

public class ArgonValidation : AbstractValidator<DefaultHashSettings>
{
    public ArgonValidation()
    {
        RuleFor(x => x.HashLength).InclusiveBetween(1, 1024);
        RuleFor(x => x.Iterations).GreaterThan(0);
        RuleFor(x => x.SaltLength).GreaterThan(0);
        RuleFor(x => x.DegreeOfParallelism).GreaterThan(0);
        RuleFor(x => x)
            .Must(x => x.MemorySize / x.DegreeOfParallelism >= 4)
            .WithMessage(
                $"For each '{nameof(DefaultHashSettings.DegreeOfParallelism)}', there should be at least 4 kilobytes of '{nameof(DefaultHashSettings.MemorySize)}'")
            .WithName(nameof(DefaultHashSettings));
    }
}