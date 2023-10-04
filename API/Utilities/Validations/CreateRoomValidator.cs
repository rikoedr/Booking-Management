using API.DataTransferObjects.Creates;
using FluentValidation;

namespace API.Utilities.Validations;

public class CreateRoomValidator : AbstractValidator<CreateRoomDTO>
{
    public CreateRoomValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty)
            .MaximumLength(100).WithMessage(Message.MaximumCharLength100);

        RuleFor(r => r.Floor)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty)
            .GreaterThan(0).WithMessage(Message.CannotLessThanEqual0);

        RuleFor(r => r.Capacity)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty)
            .GreaterThan(0).WithMessage(Message.CannotLessThanEqual0);
    }
}
