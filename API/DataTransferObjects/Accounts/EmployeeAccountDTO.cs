using API.Models;

namespace API.DataTransferObjects.Accounts;

public class EmployeeAccountDTO
{
    public Guid Guid { get; set; }
    public string NIK { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime HiringDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public bool IsDeleted { get; set; }
    public int OTP { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpiredTime { get; set; }

    public static explicit operator EmployeeAccountDTO(EmployeeAccount data)
    {
        return new EmployeeAccountDTO
        {
            Guid = data.Guid,
            NIK = data.NIK,
            FirstName = data.FirstName,
            LastName = data.LastName,
            BirthDate = data.BirthDate,
            HiringDate = data.HiringDate,
            Email = data.Email,
            PhoneNumber = data.PhoneNumber,
            Password = data.Password,
            IsDeleted = data.IsDeleted,
            OTP = data.OTP,
            IsUsed = data.IsUsed,
            ExpiredTime = data.ExpiredTime
        };
    }
}
