using FluentValidation;
using FluentValidation.Validators;
using Ordering.Application.Constants;

namespace Ordering.Application.Features.Ordering.Commands.UpdateOrder;
public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage(x => ValidationMessages.IsRequired(nameof(x.UserName)))
            .MaximumLength(70)
            .WithMessage(x => ValidationMessages.MaximumLength(nameof(x.UserName), 70));

        RuleFor(x => x.TotalPrice)
            .NotEmpty()
            .WithMessage(x => ValidationMessages.IsRequired(nameof(x.TotalPrice)))
            .GreaterThan(-1)
            .WithMessage(x => ValidationMessages.PositiveNumber(nameof(x.TotalPrice)));

        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .WithMessage(x => ValidationMessages.IsRequired(nameof(x.EmailAddress)))
            .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
            .WithMessage(x => ValidationMessages.IsEmail(nameof(x.EmailAddress)));

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(x => ValidationMessages.IsRequired(nameof(x.FirstName)));

        RuleFor(x => x.LastName)
        .NotEmpty()
        .WithMessage(x => ValidationMessages.IsRequired(nameof(x.LastName)));
    }
}
