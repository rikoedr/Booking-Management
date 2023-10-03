using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

/*
 * Role Repository merupakan class yang berfungsi untuk melakukan interaksi dengan 
 * ORM atau Database, class ini diturunkan dari Abstract Repository.
 */

public class RoleRepository : AbstractRepository<Role>, IGeneralRepository<Role>
{
    public RoleRepository(BookingManagementDbContext context) : base(context)
    {
    }
}
