using Microsoft.EntityFrameworkCore;
using WFM.Database.DbContext;
using WFM.Database.Repositories.Interfaces;

namespace WFM.Database.Repositories;

public class BaseRepository<T>(WfmContext dbContext) : IBaseRepository<T>
    where T : class
{
    private readonly DbSet<T> _entity = dbContext.Set<T>();

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _entity.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _entity.ToListAsync();
    }

    public async Task<bool> AddAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        await _entity.AddAsync(entity);
        return await SaveAsync();
    }
    
    public async Task<bool> UpdateAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        _entity.Update(entity);
        return await SaveAsync();
    }
    
    public async Task<bool> SaveAsync()
    {   
        var saved = await dbContext.SaveChangesAsync();
        return saved > 0;
    }
    
    public async Task<bool> DeleteAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        _entity.Remove(entity);
        return await SaveAsync();
    }
}