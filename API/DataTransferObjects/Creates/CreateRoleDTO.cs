using API.Models;

namespace API.DataTransferObjects.Creates;

public class CreateRoleDTO
{
    public string Name { get; set; }

    public static implicit operator Role(CreateRoleDTO createRoleDTO)
    {
        return new Role
        {
            Guid = Guid.NewGuid(),
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            Name = createRoleDTO.Name
        };
    }
}
