using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

/*
 * Employee Repository merupakan class yang berfungsi untuk melakukan interaksi dengan 
 * ORM atau Database, class ini diturunkan dari Abstract Repository.
 */

public class EmployeeRepository : AbstractRepository<Employee>, IGeneralRepository<Employee>
{
    public EmployeeRepository(BookingManagementDbContext context) : base(context)
    {
    }

    public string? GetLastNIK()
    {
        var employees = base.GetAll();

        if(employees.Count() == 0)
        {
            return null;
        }

        int result = int.Parse(employees.Last().NIK);

        return result.ToString();
    }
}
