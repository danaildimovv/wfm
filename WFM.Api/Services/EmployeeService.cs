using WFM.Api.Services.Interfaces;
using WFM.Database.Models;
using WFM.Database.Repositories.Interfaces;

namespace WFM.Api.Services;

public class EmployeeService(IEmployeeRepository employeeRepository, IEmployeesBranchesHistoryRepository employeesBranchesHistoryRepository)
    : BaseService<Employee>(employeeRepository), IEmployeeService
{
    public async Task<bool> AddEmployeeAsync(Employee employee)
    {
        var isSuccess = await employeeRepository.AddAsync(employee);
        
        if (isSuccess)
        {
            var employeeBranchesHistory = new EmployeesBranchesHistory()
            {
                EmployeeId = employee.Id,
                BranchId = employee.BranchId,
                DateStarted = DateOnly.FromDateTime(DateTime.UtcNow)
            };
            
            return await employeesBranchesHistoryRepository.AddAsync(employeeBranchesHistory);
        };
        
        return false;
    }
    
    public async Task<bool> UpdateEmployeeAsync(Employee employee)
    {
        var isSuccess = await employeeRepository.UpdateAsync(employee);
        
        if (isSuccess)
        {
            var employeeLastBranch = (await employeesBranchesHistoryRepository
                .GetBranchesByEmployeeIdAsync(employee.Id))
                .FirstOrDefault();

            if (employeeLastBranch is not null)
            {
                employeeLastBranch.DateEnded = DateOnly.FromDateTime(DateTime.UtcNow);
                isSuccess = await employeesBranchesHistoryRepository.UpdateAsync(employeeLastBranch);
                if (!isSuccess)
                {
                    return false;
                }
            }

            var employeeBranchesHistory = new EmployeesBranchesHistory()
            {
                EmployeeId = employee.Id,
                BranchId = employee.BranchId,
                DateStarted = DateOnly.FromDateTime(DateTime.UtcNow)
            };
            
            return await employeesBranchesHistoryRepository.AddAsync(employeeBranchesHistory);
        };
        
        return false;
    }
}
