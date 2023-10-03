using API.Models;

namespace API.DataTransferObjects;

public class AccountRoleDTO
{
    public Guid Guid { get; set; }
    public Guid AccountGuid { get; set; }
    public Guid RoleGuid { get; set; }

    public static explicit operator AccountRoleDTO(AccountRole accountRole)
    {
        return new AccountRoleDTO
        {
            Guid = accountRole.Guid,
            AccountGuid = accountRole.AccountGuid,
            RoleGuid = accountRole.RoleGuid
        };
    }

    public static implicit operator AccountRole(AccountRoleDTO accountRoleDTO)
    {
        return new AccountRole
        {
            Guid = accountRoleDTO.Guid,
            AccountGuid = accountRoleDTO.AccountGuid,
            RoleGuid = accountRoleDTO.RoleGuid,
            ModifiedDate = DateTime.Now
        };
    }
}
