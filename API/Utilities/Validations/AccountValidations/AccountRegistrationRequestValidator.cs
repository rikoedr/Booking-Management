using API.DataTransferObjects.Accounts;
using API.Utilities.Responses;
using FluentValidation;

namespace API.Utilities.Validations.AccountValidations;

public class AccountRegistrationRequestValidator : AbstractValidator<CreateEmployeeAccountDTO>
{
    public AccountRegistrationRequestValidator()
    {
        RuleFor(a => a.FirstName)
            .NotEmpty().WithMessage(Messages.FirstNameEmpty);

        RuleFor(a => a.LastName);

        RuleFor(a => a.BirthDate)
            .NotEmpty().WithMessage(Messages.BirthDateEmpty)
            .LessThan(DateTime.Now).WithMessage(Messages.BirthDateLessThanNow);

        RuleFor(a => a.Gender);

        RuleFor(a => a.HiringDate)
            .NotEmpty().WithMessage(Messages.HiringDateEmpty);

        RuleFor(a => a.Email)
            .NotEmpty().WithMessage(Messages.EmptyEmail)
            .EmailAddress().WithMessage(Messages.InvalidEmailFormat);

        RuleFor(a => a.PhoneNumber)
            .NotEmpty().WithMessage(Messages.PhoneNumberEmpty);

        RuleFor(a => a.Major)
            .NotEmpty().WithMessage(Messages.MajorEmpty);

        RuleFor(a => a.Degree)
            .NotEmpty().WithMessage(Messages.DegreeEmpty);

        RuleFor(a => a.GPA);

        RuleFor(a => a.UniversityCode)
            .NotEmpty().WithMessage(Messages.UniversityCodeEmpty);

        RuleFor(a => a.UniversityName)
            .NotEmpty().WithMessage(Messages.UniversityNameEmpty);

        RuleFor(a => a.Password)
            .NotEmpty().WithMessage(Messages.EmptyPassword)
            .MinimumLength(6).WithMessage(Messages.PasswordMinimumCharacter);

        RuleFor(a => a.ConfirmPassword)
            .NotEmpty().WithMessage(Messages.EmptyPassword)
            .Equal(a => a.Password).WithMessage(Messages.PasswordDoNotMatch);


    }
}
