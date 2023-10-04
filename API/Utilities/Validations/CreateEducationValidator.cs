using API.DataTransferObjects.Creates;
using FluentValidation;

namespace API.Utilities.Validations;

public class CreateEducationValidator : AbstractValidator<CreateEducationDTO>
{
    public CreateEducationValidator()
    {
        RuleFor(e => e.Guid)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty);

        RuleFor(e => e.Major)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty)
            .MaximumLength(100).WithMessage(Message.MaximumCharLength100);

        RuleFor(e => e.Degree)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty)
            .MaximumLength(100).WithMessage(Message.MaximumCharLength100);

        RuleFor(e => e.Gpa)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty);

        RuleFor(e => e.UniversityGuid)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty); ;
    }
}
