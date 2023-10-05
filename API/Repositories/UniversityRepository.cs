using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

/*
 * University Repository merupakan class yang berfungsi untuk melakukan interaksi dengan 
 * ORM atau Database, class ini diturunkan dari Abstract Repository.
 */

public class UniversityRepository : AbstractRepository<University>, IGeneralRepository<University>
{
    public UniversityRepository(BookingManagementDbContext context) : base(context)
    {
    }

    public University? GetByCode(string code)
    {
        try
        {
            if (code == null) return null;

            return base._context.Set<University>().First(u => u.Code == code);
        }
        catch
        {
            return null;
        }
    }
}
