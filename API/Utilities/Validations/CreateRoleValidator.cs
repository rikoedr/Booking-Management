using API.DataTransferObjects.Creates;
using API.Utilities.Responses;
using FluentValidation;

namespace API.Utilities.Validations;

public class CreateRoleValidator : AbstractValidator<CreateRoleDTO>
{
    public CreateRoleValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage(Messages.CanNotBeEmpty)
            .MaximumLength(100).WithMessage(Messages.MaximumCharLength100);
    }
}
