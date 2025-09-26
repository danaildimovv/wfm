using WFM.Database.Models;

namespace WFM.Database.Repositories.Interfaces;

public interface IEmployeesBranchesHistoryRepository : IBaseRepository<EmployeesBranchesHistory>
{
    Task<List<EmployeesBranchesHistory>> GetBranchesByEmployeeIdAsync(int employeeId);
    
    Task<List<EmployeesBranchesHistory>> GetEmployeesByBranchIdAsync(int branchId);
}
