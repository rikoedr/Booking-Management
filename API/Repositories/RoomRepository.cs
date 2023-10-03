using API.Contracts;
using API.Data;
using API.Model;

namespace API.Repositories;

/*
 * Room Repository merupakan class yang berfungsi untuk melakukan interaksi dengan 
 * ORM atau Database, class ini diturunkan dari Abstract Repository.
 */

public class RoomRepository : AbstractRepository<Room>, IGeneralRepository<Room>
{
    public RoomRepository(BookingManagementDbContext context) : base(context)
    {
    }
}
