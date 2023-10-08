using API.DataTransferObjects.Creates;
using API.Utilities.Responses;
using FluentValidation;

namespace API.Utilities.Validations;

public class CreateUniversityValidator : AbstractValidator<CreateUniversityDTO>
{
    public CreateUniversityValidator()
    {
        RuleFor(u => u.Code)
            .NotEmpty().WithMessage(Messages.CanNotBeEmpty)
            .MaximumLength(50).WithMessage("Maximum character length is 50");

        RuleFor(u => u.Name)
            .NotEmpty().WithMessage(Messages.CanNotBeEmpty)
            .MaximumLength(100).WithMessage(Messages.MaximumCharLength100);
    }
}
