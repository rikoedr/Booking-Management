using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

/*
 * Education Class merupakan model untuk mendifinisikan Education Object serta pengaturan anotasi ORM 
 * untuk membentuk struktur tabel "tb_m_educations"
 */

[Table("tb_m_educations")]
public class Education : AbstractModel
{
    [Column("major", TypeName = "nvarchar(100)")]
    public string Major { get; set; }

    [Column("degree", TypeName = "nvarchar(100)")]
    public string Degree { get; set; }

    [Column("gpa", TypeName = "real")]
    public float Gpa { get; set; }

    [Column("university_guid", TypeName = "uniqueidentifier")]
    public Guid UniversityGuid { get; set; }
    
    // Cardinality
    public University? University { get; set; }
    public Employee? Employee { get; set; }
}
