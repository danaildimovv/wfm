using WFM.Api.Services.Interfaces;
using WFM.Database.Repositories.Interfaces;

namespace WFM.Api.Services;

public class BaseService<T>(IBaseRepository<T> repository) : IBaseService<T> where T : class
{
    public async Task<T?> GetByIdAsync(int id)
    {
        return await repository.GetByIdAsync(id);
    }
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<bool> AddAsync(T branch)
    {
        return await repository.AddAsync(branch);
    }

    public async Task<bool> UpdateAsync(T branch)
    {
        return await repository.UpdateAsync(branch);
    }

    public async Task<bool> DeleteAsync(T branch)
    {
        return await repository.DeleteAsync(branch);
    }
}