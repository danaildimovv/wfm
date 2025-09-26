using WFM.Api.Services.Interfaces;
using WFM.Database.Models;
using WFM.Database.Repositories.Interfaces;

namespace WFM.Api.Services;

public class EmployeesBranchesHistoryService(IEmployeesBranchesHistoryRepository repository)
    : BaseService<EmployeesBranchesHistory>(repository), IEmployeesBranchesHistoryService
{
    public async Task<List<EmployeesBranchesHistory>> GetBranchesByEmployeeIdAsync(int employeeId)
    {
        return await repository.GetBranchesByEmployeeIdAsync(employeeId);
    }

    public async Task<List<EmployeesBranchesHistory>> GetEmployeesByBranchIdAsync(int branchId)
    {
        return await repository.GetEmployeesByBranchIdAsync(branchId);
    }
}
    