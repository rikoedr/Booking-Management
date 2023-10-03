using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

/*
 * Education Repository merupakan class yang berfungsi untuk melakukan interaksi dengan 
 * ORM atau Database, class ini diturunkan dari Abstract Repository.
 */

public class EducationRepository : AbstractRepository<Education>, IGeneralRepository<Education>
{
    public EducationRepository(BookingManagementDbContext context) : base(context)
    {
    }
}
