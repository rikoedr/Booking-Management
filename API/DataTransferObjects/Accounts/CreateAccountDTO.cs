using API.Models;

namespace API.DataTransferObjects.Accounts;

public class CreateAccountDTO
{
    public Guid Guid { get; set; }
    public string Password { get; set; }
    public int OTP { get; set; }

    public static implicit operator Account(CreateAccountDTO createAccountDTO)
    {
        return new Account
        {
            Guid = createAccountDTO.Guid,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            Password = createAccountDTO.Password,
            IsDeleted = false,
            IsUsed = true,
            ExpiredTime = DateTime.Now,
            OTP = createAccountDTO.OTP
        };
    }
}
