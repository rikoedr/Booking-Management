using API.DataTransferObjects.Creates;
using API.Models;
using API.Utilities;
using API.Utilities.Handlers;

namespace API.DataTransferObjects.Accounts;

public class CreateEmployeeAccountDTO
{
    private readonly Guid _guid = Guid.NewGuid();
    private readonly DateTime _createdDate = DateTime.Now;
    private readonly DateTime _modifiedDate = DateTime.Now;
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

    public static implicit operator Employee(CreateEmployeeAccountDTO request)
    {
        return new Employee
        {
            Guid = request._guid,
            CreatedDate = request._createdDate,
            ModifiedDate = request._modifiedDate,
            FirstName = request.FirstName,
            LastName = request.LastName,
            BirthDate = request.BirthDate,
            Gender = request.Gender,
            HiringDate = request.HiringDate,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber
        };
    }

    public static implicit operator Account(CreateEmployeeAccountDTO request)
    {
        return new Account
        {
            Guid = request._guid,
            CreatedDate = request._createdDate,
            ModifiedDate = request._modifiedDate,
            Password = request.Password,
            IsDeleted = false,
            IsUsed = true,
            ExpiredTime = DateTime.Now,
            OTP = 0
        };
    }

    public static implicit operator Education(CreateEmployeeAccountDTO request)
    {
        return new Education
        {
            Guid = request._guid,
            CreatedDate = request._createdDate,
            ModifiedDate = request._modifiedDate,
            Major = request.Major,
            Degree = request.Degree,
            Gpa = request.GPA
        };
    }

    public static implicit operator AccountRole(CreateEmployeeAccountDTO request)
    {
        return new AccountRole
        {
            Guid = Guid.NewGuid(),
            CreatedDate = request._createdDate,
            ModifiedDate = request._modifiedDate,
            AccountGuid = request._guid,
            RoleGuid = RoleGuids.User
        };
    }

    public static implicit operator University(CreateEmployeeAccountDTO request)
    {
        return new University
        {
            Guid = Guid.NewGuid(),
            CreatedDate = request._createdDate,
            ModifiedDate = request._modifiedDate,
            Code = request.UniversityCode,
            Name = request.UniversityName
        };
    }
}
