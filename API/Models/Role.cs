using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

/*
 * Roles Class merupakan model untuk mendifinisikan Role Object serta pengaturan anotasi ORM 
 * untuk membentuk struktur tabel "tb_m_roles"
 */

[Table("tb_m_roles")]
public class Role : AbstractModel
{
    [Column("name", TypeName = "nvarchar(100)")]
    public string Name { get; set; }

    // Cardinality
    public ICollection<AccountRole>? AccountRoles { get; set; }
}