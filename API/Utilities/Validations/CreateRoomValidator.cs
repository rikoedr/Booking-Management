using API.DataTransferObjects.Creates;
using API.Utilities.Responses;
using FluentValidation;

namespace API.Utilities.Validations;

public class CreateRoomValidator : AbstractValidator<CreateRoomDTO>
{
    public CreateRoomValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage(Messages.CanNotBeEmpty)
            .MaximumLength(100).WithMessage(Messages.MaximumCharLength100);

        RuleFor(r => r.Floor)
            .NotEmpty().WithMessage(Messages.CanNotBeEmpty)
            .GreaterThan(0).WithMessage(Messages.CannotLessThanEqual0);

        RuleFor(r => r.Capacity)
            .NotEmpty().WithMessage(Messages.CanNotBeEmpty)
            .GreaterThan(0).WithMessage(Messages.CannotLessThanEqual0);
    }
}
