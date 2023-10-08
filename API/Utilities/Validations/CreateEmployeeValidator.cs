using API.DataTransferObjects.Creates;
using API.Utilities.Responses;
using FluentValidation;

namespace API.Utilities.Validations;

public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeDTO>
{
    public CreateEmployeeValidator()
    {
        RuleFor(e => e.FirstName)
            .NotEmpty().WithMessage(Messages.CanNotBeEmpty);

        RuleFor(e => e.BirthDate)
            .NotEmpty().WithMessage(Messages.CanNotBeEmpty);

        RuleFor(e => e.Gender)
            .LessThanOrEqualTo(3);

        RuleFor(e => e.HiringDate)
            .NotEmpty().WithMessage(Messages.CanNotBeEmpty);

        RuleFor(e => e.Email)
            .NotEmpty().WithMessage(Messages.CanNotBeEmpty)
            .EmailAddress().WithMessage("Incorrect email format");

        RuleFor(e => e.PhoneNumber)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(20);
    }
}
