using API.DataTransferObjects.Creates;
using FluentValidation;

namespace API.Utilities.Validations;

public class CreateBookingValidator : AbstractValidator<CreateBookingDTO>
{
    public CreateBookingValidator()
    {
        RuleFor(b => b.StartDate)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty)
            .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Start Date cannot be less than now");

        RuleFor(b => b.EndDate)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty)
            .GreaterThan(b => b.StartDate).WithMessage("End date cannot be less than start date");

        RuleFor(b => b.Status)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty);

        RuleFor(b => b.Remarks)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty);

        RuleFor(b => b.RoomGuid)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty);

        RuleFor(b => b.EmployeeGuid)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty);
    }
}
