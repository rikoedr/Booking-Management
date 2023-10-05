using API.DataTransferObjects.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.AccountValidations;

public class AccountNewPasswordRequestValidator : AbstractValidator<AccountNewPasswordRequestDTO>
{
    public AccountNewPasswordRequestValidator()
    {
        RuleFor(a => a.OTP)
            .NotEmpty().WithMessage(Message.EmptyOTP)
            .LessThan(1000000).WithMessage(Message.InvalidOTPFormat)
            .GreaterThan(99999).WithMessage(Message.InvalidOTPFormat);

        RuleFor(a => a.Email)
            .NotEmpty().WithMessage(Message.EmptyEmail)
            .EmailAddress().WithMessage(Message.InvalidEmailFormat);

        RuleFor(a => a.NewPassword)
            .NotEmpty().WithMessage(Message.EmptyPassword);

        RuleFor(a => a.ConfirmPassword)
            .NotEmpty().WithMessage(Message.EmptyPassword)
            .Equal(a => a.NewPassword).WithMessage(Message.PasswordDoNotMatch);
    }
}
