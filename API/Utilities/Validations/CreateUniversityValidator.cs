using API.DataTransferObjects.Creates;
using FluentValidation;

namespace API.Utilities.Validations;

public class CreateUniversityValidator : AbstractValidator<CreateUniversityDTO>
{
    public CreateUniversityValidator()
    {
        RuleFor(u => u.Code)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty)
            .MaximumLength(50).WithMessage("Maximum character length is 50");

        RuleFor(u => u.Name)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty)
            .MaximumLength(100).WithMessage(Message.MaximumCharLength100);
    }
}
