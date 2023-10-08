using API.DataTransferObjects.Accounts;
using API.Utilities.Responses;
using FluentValidation;

namespace API.Utilities.Validations.AccountValidations;

public class AccountEmailRequestValidator : AbstractValidator<AccountEmailRequestDTO>
{
    public AccountEmailRequestValidator()
    {
        RuleFor(a => a.Email)
            .NotEmpty().WithMessage(Messages.EmptyEmail)
            .EmailAddress().WithMessage(Messages.InvalidEmailFormat);
    }
}
