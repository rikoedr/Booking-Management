using API.DataTransferObjects.Accounts;
using API.Utilities.Responses;
using FluentValidation;

namespace API.Utilities.Validations.AccountValidations;

public class AccountNewPasswordRequestValidator : AbstractValidator<AccountNewPasswordRequestDTO>
{
    public AccountNewPasswordRequestValidator()
    {
        RuleFor(a => a.OTP)
            .NotEmpty().WithMessage(Messages.EmptyOTP)
            .LessThan(1000000).WithMessage(Messages.InvalidOTPFormat)
            .GreaterThan(99999).WithMessage(Messages.InvalidOTPFormat);

        RuleFor(a => a.Email)
            .NotEmpty().WithMessage(Messages.EmptyEmail)
            .EmailAddress().WithMessage(Messages.InvalidEmailFormat);

        RuleFor(a => a.NewPassword)
            .NotEmpty().WithMessage(Messages.EmptyPassword);

        RuleFor(a => a.ConfirmPassword)
            .NotEmpty().WithMessage(Messages.EmptyPassword)
            .Equal(a => a.NewPassword).WithMessage(Messages.PasswordDoNotMatch);
    }
}
