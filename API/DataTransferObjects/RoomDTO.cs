using API.Models;

namespace API.DataTransferObjects;

public class RoomDTO
{
    public Guid Guid { get; set; }
    public string Name { get; set; }

    public int Floor { get; set; }

    public int Capacity { get; set; }

    public static explicit operator RoomDTO(Room room)
    {
        return new RoomDTO
        {
            Guid = room.Guid,
            Name = room.Name,
            Floor = room.Floor,
            Capacity = room.Capacity
        };
    }

    public static implicit operator Room(RoomDTO roomDTO)
    {
        return new Room
        {
            Guid = roomDTO.Guid,
            Name = roomDTO.Name,
            Floor = roomDTO.Floor,
            Capacity = roomDTO.Capacity,
            ModifiedDate = DateTime.Now
        };
    }
}
