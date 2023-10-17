using API.DataTransferObjects;
using API.Models;

namespace Client.Contracts;

public interface IEmployeeRepository : IRepository<EmployeeDTO, Guid>
{
}
