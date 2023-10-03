using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

/*
 * Account Class merupakan model untuk mendifinisikan Account Object serta pengaturan anotasi ORM 
 * untuk membentuk struktur tabel "tb_m_account"
 */

[Table("tb_m_accounts")]
public class Account : AbstractModel
{
    [Column("password", TypeName = "nvarchar(max)")]
    public string Password { get; set; }
    
    [Column("is_deleted", TypeName = "bit")]
    public Boolean IsDeleted { get; set; }

    [Column("otp", TypeName = "int")]
    public int OTP { get; set; }

    [Column("is_used", TypeName = "bit")]
    public Boolean IsUsed { get; set; }

    [Column("expired_time", TypeName = "datetime")]
    public DateTime ExpiredTime { get; set; }

    // Cardinality
    public Employee? Employee { get; set; }
    public ICollection<AccountRole>? AccountRoles { get; set; }
}
