namespace API.DataTransferObjects.Accounts;

public class AccountNewPasswordRequestDTO
{
    public string Email { get; set; }
    public int OTP { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}
