using WFM.Database.Models;

namespace WFM.Database.Repositories.Interfaces;

public interface IEmployeesJobHistoryRepository : IBaseRepository<EmployeesJobHistory>
{
    Task<List<EmployeesJobHistory>> GetJobsByEmployeeIdAsync(int employeeId);
    
    Task<List<EmployeesJobHistory>> GetEmployeesByJobIdAsync(int jobId);
}
