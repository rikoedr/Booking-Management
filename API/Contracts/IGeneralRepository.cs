namespace API.Contracts;

/*
 * IGeneralRepository merupakan interface yang berisi kontrak method
 * yang general atau umum untuk semua repository yang mengimplementasikan
 * interface ini.
 */
public interface IGeneralRepository<TEntity> where TEntity : class
{
    IEnumerable<TEntity> GetAll();
    TEntity? GetByGuid(Guid guid);
    TEntity? Create(TEntity entity);
    bool Update(TEntity entity);
    bool Delete(TEntity entity);
}
