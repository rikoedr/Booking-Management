using API.Models;

namespace API.DataTransferObjects.Accounts;

public class AccountDTO
{
    public Guid Guid { get; set; }
    public string Password { get; set; }
    public bool IsDeleted { get; set; }
    public int OTP { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpiredTime { get; set; }

    public static explicit operator AccountDTO(Account account)
    {
        return new AccountDTO
        {
            Guid = account.Guid,
            Password = account.Password,
            IsDeleted = account.IsDeleted,
            OTP = account.OTP,
            IsUsed = account.IsUsed,
            ExpiredTime = account.ExpiredTime
        };
    }

    public static implicit operator Account(AccountDTO accountDTO)
    {
        return new Account
        {
            Guid = accountDTO.Guid,
            Password = accountDTO.Password,
            IsDeleted = accountDTO.IsDeleted,
            OTP = accountDTO.OTP,
            IsUsed = accountDTO.IsUsed,
            ExpiredTime = accountDTO.ExpiredTime,
            ModifiedDate = DateTime.Now
        };
    }

}
