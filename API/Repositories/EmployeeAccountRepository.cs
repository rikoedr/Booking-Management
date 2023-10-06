using API.Contracts;
using API.Data;
using API.Models;
using API.Utilities;
using API.Utilities.Handlers;

namespace API.Repositories;

/*
 * EmployeeAccount Repository adalah repository yang berfungsi untuk 
 * melakukan operasi gabungan yang melibatkan dua entitas yaitu Employee dan Account.
 * Dengan cara ini operasi yang dilakukan dapat secara bersamaan sehingga jika terjadi
 * error pada salah satu operasi entitas maka perubahan dalam .SaveChanges tidak
 * akan di tulis ke database.
 */

public class EmployeeAccountRepository
{
    private readonly BookingManagementDbContext _context;

    public EmployeeAccountRepository(BookingManagementDbContext context)
    {
        _context = context;
    }

    /* Create dalam EmployeeAccount Repository berfungsi untuk melakukan
     * pembuatan data dalam database untuk dua table yaitu Employee dan Account.
     * 
     * Dengan melakukan penulisan data dalam satu blok try yang sama untuk ke dua
     * table maka method .SaveChanges() berfungsi sebagai Commit & Rollback.
     * 
     * Jika dalam penulisan employee atau account salah satunya terjadi kegagalan
     * maka akan dilemparkan Exception dan method .SaveChanges() tidak akan memproses
     * perubahan yang ada.
     */
    public EmployeeAccount? Create(EmployeeAccount entity)
    {
        
        try
        {
            // Buat entity employee dan account terpisah dari model EmployeeAccount (Implicit Operator)
            Employee employee = entity;
            Account account = entity;

            // Tambahkan data dalam antrian penulisan ke database
            _context.Set<Employee>().Add(employee);
            _context.Set<Account>().Add(account);
            // Proses antrian operasi database
            int commitTransaction;

            // Try Catch proses penulisan database
            try
            {
                commitTransaction = _context.SaveChanges();
            }
            catch
            {
                commitTransaction = 0;
            }

            // Throw error jika proses penulisan database gagal
            if (commitTransaction <= 0)
            {
                throw new ExceptionHandler("Transaction in creating Employee and Account Data Failed");
            }

            // Proses berhasil kembalikan entity
            return entity;
            
        }
        catch
        {
            //Kembalikan null jika terjadi exception
            return null;
        }
    }
}
