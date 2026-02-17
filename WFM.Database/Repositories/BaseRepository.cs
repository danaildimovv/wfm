using Microsoft.EntityFrameworkCore;
using Npgsql;
using WFM.Api.Exceptions;
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
        return await _entity
            .OrderBy(x => EF.Property<int>(x, "Id"))
            .ToListAsync();
    }

    public async Task<bool> AddAsync(T entity)
    {
        await _entity.AddAsync(entity);
        return await SaveAsync();
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        _entity.Attach(entity);
        dbContext.Entry(entity).State = EntityState.Modified;

        return await SaveAsync();
    }

    public async Task<bool> SaveAsync()
    {
        var saved = await dbContext.SaveChangesAsync();
        return saved > 0;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        _entity.Remove(entity);
        
        try
        {
            return await SaveAsync();
        }
        catch(DbUpdateException ex) when (IsFkViolation(ex))
        {
            throw new ResourceInUseException(
                "The entity cannot be deleted because it is referenced by other data."
            );
        }
    }

    private static bool IsFkViolation(DbUpdateException ex)
    {
        return ex.InnerException is PostgresException pgEx
               && pgEx.SqlState == PostgresErrorCodes.ForeignKeyViolation;
    }
}