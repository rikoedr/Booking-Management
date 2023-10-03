using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

/*
 * Booking Repository merupakan class yang berfungsi untuk melakukan interaksi dengan 
 * ORM atau Database, class ini diturunkan dari Abstract Repository.
 */

public class BookingRepository : AbstractRepository<Booking>, IGeneralRepository<Booking>
{
    public BookingRepository(BookingManagementDbContext context) : base(context)
    {
    }
}
