using API.DataTransferObjects.Creates;
using API.Utilities.Responses;
using FluentValidation;

namespace API.Utilities.Validations;

public class CreateEducationValidator : AbstractValidator<CreateEducationDTO>
{
    public CreateEducationValidator()
    {
        RuleFor(e => e.Guid)
            .NotEmpty().WithMessage(Messages.CanNotBeEmpty);

        RuleFor(e => e.Major)
            .NotEmpty().WithMessage(Messages.CanNotBeEmpty)
            .MaximumLength(100).WithMessage(Messages.MaximumCharLength100);

        RuleFor(e => e.Degree)
            .NotEmpty().WithMessage(Messages.CanNotBeEmpty)
            .MaximumLength(100).WithMessage(Messages.MaximumCharLength100);

        RuleFor(e => e.Gpa)
            .NotEmpty().WithMessage(Messages.CanNotBeEmpty);

        RuleFor(e => e.UniversityGuid)
            .NotEmpty().WithMessage(Messages.CanNotBeEmpty); ;
    }
}
