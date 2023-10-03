using API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DataTransferObjects.Creates;

public class CreateAccountRoleDTO
{
    public Guid AccountGuid { get; set; }

    public Guid RoleGuid { get; set; }

    public static implicit operator AccountRole(CreateAccountRoleDTO createAccountRoleDTO)
    {
        return new AccountRole
        {
            Guid = Guid.NewGuid(),
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            AccountGuid = createAccountRoleDTO.AccountGuid,
            RoleGuid = createAccountRoleDTO.RoleGuid
        };
    }
}
