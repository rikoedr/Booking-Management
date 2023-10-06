using API.DataTransferObjects.Creates;
using API.Models;
using API.Utilities.Handlers;

namespace API.DataTransferObjects.EmployeeAccounts;

public class CreateEmployeeAccountDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public int Gender { get; set; }
    public DateTime HiringDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Major { get; set; }
    public string Degree { get; set; }
    public float GPA { get; set; }
    public string UniversityCode { get; set; }
    public string UniversityName { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    public static implicit operator EmployeeAccount(CreateEmployeeAccountDTO request)
    {
        return new EmployeeAccount
        {
            Guid = Guid.NewGuid(),
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            FirstName = request.FirstName,
            LastName = request.LastName,
            BirthDate = request.BirthDate,
            Gender = request.Gender,
            HiringDate = request.HiringDate,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Password = request.Password,
            IsDeleted = false,
            IsUsed = true,
            ExpiredTime = DateTime.Now,
            OTP = 0
        };
    }
}
