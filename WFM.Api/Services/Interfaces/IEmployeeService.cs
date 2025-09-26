using WFM.Database.Models;

namespace WFM.Api.Services.Interfaces;

public interface IEmployeeService : IBaseService<Employee>
{
    Task<bool> AddEmployeeAsync(Employee employee);
    
    Task<bool> UpdateEmployeeAsync(Employee employee);
}
