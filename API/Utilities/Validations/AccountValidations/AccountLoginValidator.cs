using API.DataTransferObjects.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.AccountValidations
{
    public class AccountLoginValidator : AbstractValidator<AccountLoginRequestDTO>
    {
        public AccountLoginValidator()
        {
            RuleFor(a => a.Email)
                .NotEmpty().WithMessage(Message.EmptyEmail)
                .EmailAddress().WithMessage(Message.InvalidEmailFormat);

            RuleFor(a => a.Password)
                .NotEmpty().WithMessage(Message.EmptyPassword);
        }
    }
}
