using API.DataTransferObjects.Creates;
using FluentValidation;

namespace API.Utilities.Validations;

public class CreateRoleValidator : AbstractValidator<CreateRoleDTO>
{
    public CreateRoleValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty)
            .MaximumLength(100).WithMessage(Message.MaximumCharLength100);
    }
}
