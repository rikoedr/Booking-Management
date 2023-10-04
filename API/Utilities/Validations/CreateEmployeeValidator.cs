using API.DataTransferObjects.Creates;
using FluentValidation;

namespace API.Utilities.Validations;

public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeDTO>
{
    public CreateEmployeeValidator()
    {
        RuleFor(e => e.FirstName)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty);
        RuleFor(e => e.BirthDate)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty);
        RuleFor(e => e.Gender)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty);
        RuleFor(e => e.HiringDate)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty);

        RuleFor(e => e.Email)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty)
            .EmailAddress().WithMessage("Incorrect email format");

        RuleFor(e => e.PhoneNumber)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(10);
    }
}
