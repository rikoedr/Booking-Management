using API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model;

/*
 * Room Class merupakan model untuk mendifinisikan Room Object serta pengaturan anotasi ORM 
 * untuk membentuk struktur tabel "tb_m_rooms"
 */

[Table("tb_m_rooms")]
public class Room : AbstractModel{
    [Column("name", TypeName = "nvarchar(100)")]
    public string Name { get; set; }

    [Column("floor", TypeName = "int")]
    public int Floor { get; set; }

    [Column("capacity", TypeName = "int")]
    public int Capacity { get; set; }

    // Cardinality
    public ICollection<Booking>? Bookings { get; set; }
}
