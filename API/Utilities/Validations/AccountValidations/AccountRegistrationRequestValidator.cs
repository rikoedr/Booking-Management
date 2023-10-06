using API.DataTransferObjects.EmployeeAccounts;
using FluentValidation;

namespace API.Utilities.Validations.AccountValidations;

public class AccountRegistrationRequestValidator : AbstractValidator<CreateEmployeeAccountDTO>
{
    public AccountRegistrationRequestValidator()
    {
        RuleFor(a => a.FirstName)
            .NotEmpty().WithMessage(Message.FirstNameEmpty);

        RuleFor(a => a.LastName);

        RuleFor(a => a.BirthDate)
            .NotEmpty().WithMessage(Message.BirthDateEmpty)
            .LessThan(DateTime.Now).WithMessage(Message.BirthDateLessThanNow);

        RuleFor(a => a.Gender);

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

        RuleFor(a => a.GPA);

        RuleFor(a => a.UniversityCode)
            .NotEmpty().WithMessage(Message.UniversityCodeEmpty);

        RuleFor(a => a.UniversityName)
            .NotEmpty().WithMessage(Message.UniversityNameEmpty);

        RuleFor(a => a.Password)
            .NotEmpty().WithMessage(Message.EmptyPassword)
            .MinimumLength(6).WithMessage(Message.PasswordMinimumCharacter);

        RuleFor(a => a.ConfirmPassword)
            .NotEmpty().WithMessage(Message.EmptyPassword)
            .Equal(a => a.Password).WithMessage(Message.PasswordDoNotMatch);


    }
}
