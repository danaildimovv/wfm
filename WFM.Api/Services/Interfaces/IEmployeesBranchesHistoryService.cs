using WFM.Database.Models;

namespace WFM.Api.Services.Interfaces;

public interface IEmployeesBranchesHistoryService : IBaseService<EmployeesBranchesHistory>
{
    Task<List<EmployeesBranchesHistory>> GetBranchesByEmployeeIdAsync(int employeeId);
    
    Task<List<EmployeesBranchesHistory>> GetEmployeesByBranchIdAsync(int branchId);
}
