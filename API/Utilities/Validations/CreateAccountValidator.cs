using API.DataTransferObjects.Accounts;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace API.Utilities.Validations;

/*
 * CreateAccountValidator adalah class yang berfungsi untuk melakukan validasi terhadap data yang dimasukkan
 * dalam DTO sebelum diproses oleh controller. Tiap attribute akan diberikan Rule atau aturan sesuai
 * dengan kebutuhan yang ada, rule dilengkapi message untuk memberitahukan kesalahan validasi yang terjadi. 
 */

public class CreateAccountValidator : AbstractValidator<CreateAccountDTO>
{
    public CreateAccountValidator()
    {
        RuleFor(a => a.Guid)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty);

        RuleFor(a => a.OTP);

        RuleFor(a => a.Password)
            .NotEmpty().WithMessage(Message.CanNotBeEmpty)
            .MinimumLength(6).WithMessage("Password must be a minimum of 6 characters");
    }
}
