using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

/*
 * University Class merupakan model untuk mendifinisikan University Object serta pengaturan anotasi ORM 
 * untuk membentuk struktur tabel "tb_m_universities"
 */

[Table("tb_m_universities")]
public class University : AbstractModel
{
    [Column("code", TypeName = "nvarchar(50)")]
    public string Code { get; set; }

    [Column("name", TypeName = "nvarchar(100)")]
    public string Name { get; set; }

    // Cardinality
    public ICollection<Education>? Educations { get; set; }
}
