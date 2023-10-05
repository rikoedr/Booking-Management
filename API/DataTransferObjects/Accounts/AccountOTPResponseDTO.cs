using API.Models;

namespace API.DataTransferObjects.Accounts;

public class AccountOTPResponseDTO
{
    public int OTP { get; set; }
    public DateTime ExpiredDate { get; set; }

    public static explicit operator AccountOTPResponseDTO(Account account)
    {
        return new AccountOTPResponseDTO
        {
            OTP = account.OTP,
            ExpiredDate = account.ExpiredTime
        };
    }
}
