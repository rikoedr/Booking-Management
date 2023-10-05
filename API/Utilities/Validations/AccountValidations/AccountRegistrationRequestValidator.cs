using API.DataTransferObjects.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.AccountValidations;

public class AccountRegistrationRequestValidator : AbstractValidator<AccountRegistrationRequestDTO>
{
    public AccountRegistrationRequestValidator()
    {
        RuleFor(a => a.FirstName)
            .NotEmpty().WithMessage(Message.FirstNameEmpty);

        RuleFor(a => a.BirthDate)
            .NotEmpty().WithMessage(Message.BirthDateEmpty);

        RuleFor(a => a.Gender)
            .NotEmpty().WithMessage(Message.GenderEmpty);

        RuleFor(a => a.HiringDate)
            .NotEmpty().WithMessage(Message.HiringDateEmpty);

        RuleFor(a => a.Email)
            .NotEmpty().WithMessage(Message.EmptyEmail)
            .EmailAddress().WithMessage(Message.InvalidEmailFormat);

        RuleFor(a => a.PhoneNumber)
            .NotEmpty().WithMessage(Message.PhoneNumberEmpty);

        RuleFor(a => a.Major)
            .NotEmpty().WithMessage(Message.MajorEmpty);

        RuleFor(a => a.Degree)
            .NotEmpty().WithMessage(Message.DegreeEmpty);

        RuleFor(a => a.GPA)
            .NotEmpty().WithMessage(Message.GPAEmpty);

        RuleFor(a => a.UniversityCode)
            .NotEmpty().WithMessage(Message.UniversityCodeEmpty);

        RuleFor(a => a.UniversityName)
            .NotEmpty().WithMessage(Message.UniversityNameEmpty);

        RuleFor(a => a.Password)
            .NotEmpty().WithMessage(Message.EmptyPassword);

        RuleFor(a => a.ConfirmPassword)
            .NotEmpty().WithMessage(Message.EmptyPassword)
            .Equal(a => a.Password).WithMessage(Message.PasswordDoNotMatch);


    }
}
