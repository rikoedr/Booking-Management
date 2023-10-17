using API.DataTransferObjects;
using API.Models;
using Client.Contracts;

namespace Client.Repositories;

public class EmployeeRepository : GeneralRepository<EmployeeDTO, Guid>, IEmployeeRepository
{
    public EmployeeRepository(string request = "Employee/") : base(request)
    {
    }
}
