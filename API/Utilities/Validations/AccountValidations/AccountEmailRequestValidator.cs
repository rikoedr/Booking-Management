using API.DataTransferObjects.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.AccountValidations;

public class AccountEmailRequestValidator : AbstractValidator<AccountEmailRequestDTO>
{
    public AccountEmailRequestValidator()
    {
        RuleFor(a => a.Email)
            .NotEmpty().WithMessage(Message.EmptyEmail)
            .EmailAddress().WithMessage(Message.InvalidEmailFormat);
    }
}
