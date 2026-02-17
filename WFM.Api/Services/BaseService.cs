using WFM.Api.Services.Interfaces;
using WFM.Database.Repositories.Interfaces;

namespace WFM.Api.Services;

public class BaseService<T>(IBaseRepository<T> repository) : IBaseService<T> where T : class
{
    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await repository.GetByIdAsync(id);
        ArgumentNullException.ThrowIfNull(entity);
        
        return entity;
    }
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<bool> AddAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        return await repository.AddAsync(entity);
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        return await repository.UpdateAsync(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        ArgumentNullException.ThrowIfNull(entity);
        
        return await repository.DeleteAsync(entity);
    }
}