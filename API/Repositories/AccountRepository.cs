using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

/*
 * Account Repository merupakan class yang berfungsi untuk melakukan interaksi dengan 
 * ORM atau Database, class ini diturunkan dari Abstract Repository.
 */

public class AccountRepository : AbstractRepository<Account>, IGeneralRepository<Account>
{
    public AccountRepository(BookingManagementDbContext context) : base(context)
    {
    }
}
