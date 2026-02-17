namespace WFM.Api.Services.Interfaces;

public interface IBaseService<T> where T : class
{
    public Task<T> GetByIdAsync(int id);
    public Task<IEnumerable<T>> GetAllAsync();
    public Task<bool> AddAsync(T entity);
    
    public Task<bool> UpdateAsync(T entity);
    
    public Task<bool> DeleteAsync(int id);
}