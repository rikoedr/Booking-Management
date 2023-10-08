using API.DataTransferObjects.Accounts;
using API.Utilities.Responses;
using FluentValidation;

namespace API.Utilities.Validations.AccountValidations
{
    public class AccountLoginValidator : AbstractValidator<AccountLoginRequestDTO>
    {
        public AccountLoginValidator()
        {
            RuleFor(a => a.Email)
                .NotEmpty().WithMessage(Messages.EmptyEmail)
                .EmailAddress().WithMessage(Messages.InvalidEmailFormat);

            RuleFor(a => a.Password)
                .NotEmpty().WithMessage(Messages.EmptyPassword);
        }
    }
}
