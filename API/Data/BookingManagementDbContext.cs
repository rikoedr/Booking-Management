namespace API.Data;

using API.Model;
using API.Models;
using Microsoft.EntityFrameworkCore;

public class BookingManagementDbContext : DbContext
{
    public BookingManagementDbContext(DbContextOptions<BookingManagementDbContext> options)
        : base(options) { }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings {get; set;}
    public DbSet<Education> Educations { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<University> Universities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Constraint Configuration
        modelBuilder.Entity<Employee>().HasIndex(e => new
        {
            e.NIK,
            e.Email,
            e.PhoneNumber
        }).IsUnique();

        // Cardinality Configuration
        modelBuilder.Entity<University>()
            .HasMany(e => e.Educations)
            .WithOne(u => u.University)
            .HasForeignKey(e => e.UniversityGuid)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Education>()
            .HasOne(e => e.Employee)
            .WithOne(e => e.Education)
            .HasForeignKey<Education>(e => e.Guid);

        modelBuilder.Entity<Employee>()
            .HasOne(a => a.Account)
            .WithOne(e => e.Employee)
            .HasForeignKey<Employee>(e => e.Guid);

        modelBuilder.Entity<Employee>()
            .HasMany(b => b.Bookings)
            .WithOne(e => e.Employee)
            .HasForeignKey(b => b.EmployeeGuid);

        modelBuilder.Entity<Room>()
            .HasMany(b => b.Bookings)
            .WithOne(r => r.Room)
            .HasForeignKey(b => b.RoomGuid);

        modelBuilder.Entity<Account>()
            .HasMany(ar => ar.AccountRoles)
            .WithOne(a => a.Account)
            .HasForeignKey(ar => ar.AccountGuid);

        modelBuilder.Entity<Role>()
            .HasMany(ar => ar.AccountRoles)
            .WithOne(r => r.Role)
            .HasForeignKey(ar => ar.RoleGuid);
    }
}
