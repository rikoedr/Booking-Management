using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

/*
 * EmployeeAccount adalah model untuk menampung format gabungan dari model Employee dan Account.
 * Model ini akan digunakan untuk fitur-fitur yang membutuhkan gabungan data dari kedua table tersebut.
 */

public class EmployeeAccount
{
    public Guid Guid { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string NIK { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public int Gender { get; set; }
    public DateTime HiringDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public bool IsDeleted { get; set; }
    public int OTP { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpiredTime { get; set; }

    // Operator untuk konversi data ke tipe data Employee
    public static implicit operator Employee(EmployeeAccount data)
    {
        return new Employee
        {
            Guid = data.Guid,
            CreatedDate = data.CreatedDate,
            ModifiedDate = data.ModifiedDate,
            NIK = data.NIK,
            FirstName = data.FirstName,
            LastName = data.LastName,
            BirthDate = data.BirthDate,
            Gender = data.Gender,
            HiringDate = data.HiringDate,
            Email = data.Email,
            PhoneNumber = data.PhoneNumber,
        };
    }

    // Operator untuk konversi data ke tipe data Account
    public static implicit operator Account(EmployeeAccount data)
    {
        return new Account
        {
            Guid = data.Guid,
            CreatedDate = data.CreatedDate,
            ModifiedDate = data.ModifiedDate,
            Password = data.Password,
            IsDeleted = data.IsDeleted,
            OTP = data.OTP,
            IsUsed = data.IsUsed,
            ExpiredTime = data.ExpiredTime
        };
    }
}
