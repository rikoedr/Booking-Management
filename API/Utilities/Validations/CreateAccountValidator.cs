using API.DataTransferObjects.Creates;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace API.Utilities.Validations;

public class CreateAccountValidator : AbstractValidator<CreateAccountDTO>
{
    public CreateAccountValidator()
    {
        RuleFor(a => a.Guid)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty);

        RuleFor(a => a.OTP)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty);

        RuleFor(a => a.Password)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty)
            .MinimumLength(6).WithMessage("Password must be a minimum of 6 characters");
    }
}
