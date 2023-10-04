
using API.Models;

namespace API.DataTransferObjects.Creates;

public class CreateRoomDTO
{
    public string Name { get; set; }

    public int Floor { get; set; }

    public int Capacity { get; set; }

    public static implicit operator Room(CreateRoomDTO createRoomDTO)
    {
        return new Room
        {
            Guid = Guid.NewGuid(),
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            Name = createRoomDTO.Name,
            Floor = createRoomDTO.Floor,
            Capacity = createRoomDTO.Capacity
        };
    }
}
