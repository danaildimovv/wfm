using WFM.Database.Models;

namespace WFM.Api.Services.Interfaces;

public interface IEmployeesJobHistoryService : IBaseService<EmployeesJobHistory>
{
    Task<List<EmployeesJobHistory>> GetJobsByEmployeeIdAsync(int employeeId);
    
    Task<List<EmployeesJobHistory>> GetEmployeesByJobIdAsync(int jobId);
}
