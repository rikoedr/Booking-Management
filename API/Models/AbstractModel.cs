using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

/*
 * Abstract Model adalah kelas abstract yang berfungsi untuk mendifinisikan objek atribut dan anotasi terkait ORM
 * yang bersifat umum atau dimiliki oleh semua turunannya.
 */
public abstract class AbstractModel
{
    [Column("created_date", TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column("modified_date", TypeName = "datetime")]
    public DateTime ModifiedDate { get; set; }

    [Key]
    [Column("guid", TypeName = "uniqueidentifier")]
    public Guid Guid { get; set; }
}
