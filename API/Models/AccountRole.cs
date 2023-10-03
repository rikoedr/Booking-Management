using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

/*
 * Account Roles Class merupakan model untuk mendifinisikan Account Role Object serta pengaturan anotasi ORM 
 * untuk membentuk struktur tabel "tb_m_account_roles"
 */

[Table("tb_m_account_roles")]
public class AccountRole : AbstractModel
{
    [Column("account_guid", TypeName = "uniqueidentifier")]
    public Guid AccountGuid { get; set; }

    [Column("role_guid", TypeName = "uniqueidentifier")]
    public Guid RoleGuid { get; set; }

    // Cardinality
    public Account? Account { get; set; }
    public Role? Role { get; set; }
}
