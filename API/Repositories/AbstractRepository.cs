using API.Contracts;
using API.Data;

/*
 * Abstract Repository adalah abstract class yang digunakan untuk mengatur
 * fungsi repository umum yang dimiliki oleh turunannya yaitu :
 * 
 * Get All (Collection), Get By Guid (Single), Create (Single), Update & Delete (Boolean)
 */

namespace API.Repositories;
public abstract class AbstractRepository<TEntity> : IGeneralRepository<TEntity>
    where TEntity : class
{
    private readonly BookingManagementDbContext _context;

    protected AbstractRepository(BookingManagementDbContext context)
    {
        _context = context;
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _context.Set<TEntity>().ToList();
    }

    public TEntity? GetByGuid(Guid guid)
    {
        TEntity? entity = _context.Set<TEntity>().Find(guid);
        _context.ChangeTracker.Clear();

        return entity;
    }

    public TEntity? Create(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();

            return entity;
        }
        catch
        {
            return null;
        }
    }

    public bool Update(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();

            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Delete(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();

            return true;
        }
        catch
        {
            return false;
        }
    }

}
