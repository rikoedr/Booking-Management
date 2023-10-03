using API.Models;

namespace API.DataTransferObjects.Creates;

public class CreateEmployeeDTO
{
    public string NIK { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime HiringDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public static implicit operator Employee(CreateEmployeeDTO employeeDTO)
    {
        return new Employee
        {
            Guid = Guid.NewGuid(),
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            NIK = employeeDTO.NIK,
            FirstName = employeeDTO.FirstName,
            LastName = employeeDTO.LastName,
            BirthDate = employeeDTO.BirthDate,
            HiringDate = employeeDTO.HiringDate,
            Email = employeeDTO.Email,
            PhoneNumber = employeeDTO.PhoneNumber
        };
    }
}
