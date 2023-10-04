using API.Data;
using API.DataTransferObjects.Creates;
using FluentValidation;

namespace API.Utilities.Validations;

public class CreateAccountRoleValidator : AbstractValidator<CreateAccountRoleDTO>
{
    public CreateAccountRoleValidator()
    {
        RuleFor(ar => ar.AccountGuid)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty);

        RuleFor(ar => ar.RoleGuid)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty);
    }
}
