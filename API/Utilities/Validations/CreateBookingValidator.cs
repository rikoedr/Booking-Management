using API.DataTransferObjects.Creates;
using API.Utilities.Responses;
using FluentValidation;

namespace API.Utilities.Validations;

public class CreateBookingValidator : AbstractValidator<CreateBookingDTO>
{
    public CreateBookingValidator()
    {
        RuleFor(b => b.StartDate)
            .NotEmpty().WithMessage(Messages.CanNotBeEmpty);

        RuleFor(b => b.EndDate)
            .NotEmpty().WithMessage(Messages.CanNotBeEmpty);

        RuleFor(b => b.Status)
            .NotEmpty().WithMessage(Messages.CanNotBeEmpty);

        RuleFor(b => b.Remarks)
            .NotEmpty().WithMessage(Messages.CanNotBeEmpty);

        RuleFor(b => b.RoomGuid)
            .NotEmpty().WithMessage(Messages.CanNotBeEmpty);

        RuleFor(b => b.EmployeeGuid)
            .NotEmpty().WithMessage(Messages.CanNotBeEmpty);
    }
}
