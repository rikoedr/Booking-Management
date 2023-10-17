using API.Models;
using API.Utilities.Handlers;

namespace API.DataTransferObjects;

public class EmployeeDTO
{
    public Guid Guid { get; set; }
    public string NIK { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime HiringDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public static explicit operator EmployeeDTO(Employee employee)
    {
        return new EmployeeDTO
        {
            Guid = employee.Guid,
            NIK = employee.NIK,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Gender = GenderHandler.toString(employee.Gender),
            BirthDate = employee.BirthDate,
            HiringDate = employee.HiringDate,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber
        };
    }

    public static implicit operator Employee(EmployeeDTO employeeDTO)
    {
        return new Employee
        {
            Guid = employeeDTO.Guid,
            NIK = employeeDTO.NIK,
            FirstName = employeeDTO.FirstName,
            LastName = employeeDTO.LastName,
            BirthDate = employeeDTO.BirthDate,
            HiringDate = employeeDTO.HiringDate,
            Email = employeeDTO.Email,
            PhoneNumber = employeeDTO.PhoneNumber,
            ModifiedDate = DateTime.Now
        };
    }
}
