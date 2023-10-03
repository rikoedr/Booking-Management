using API.Models;

namespace API.DataTransferObjects;

public class RoleDTO
{
    public Guid Guid { get; set; }
    public string Name { get; set; }

    public static explicit operator RoleDTO(Role role)
    {
        return new RoleDTO
        {
            Guid = role.Guid,
            Name = role.Name
        };
    }

    public static implicit operator Role(RoleDTO roleDTO)
    {
        return new Role
        {
            Guid = roleDTO.Guid,
            Name = roleDTO.Name,
            ModifiedDate = DateTime.Now
        };
    }

}
