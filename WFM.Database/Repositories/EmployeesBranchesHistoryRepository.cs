using Microsoft.EntityFrameworkCore;
using WFM.Database.DbContext;
using WFM.Database.Models;
using WFM.Database.Repositories.Interfaces;

namespace WFM.Database.Repositories;

public class EmployeesBranchesHistoryRepository(WfmContext dbContext)
    : BaseRepository<EmployeesBranchesHistory>(dbContext), IEmployeesBranchesHistoryRepository
{
    private readonly DbSet<EmployeesBranchesHistory> _entity = dbContext.Set<EmployeesBranchesHistory>();
    
    public async Task<List<EmployeesBranchesHistory>> GetBranchesByEmployeeIdAsync(int employeeId)
    {
        return await _entity
            .Where(x => x.EmployeeId == employeeId)
            .OrderByDescending(x => x.DateStarted)
            .ToListAsync();
    }
    
    public async Task<List<EmployeesBranchesHistory>> GetEmployeesByBranchIdAsync(int branchId)
    {
        return await _entity
            .Where(x => x.BranchId == branchId)
            .ToListAsync();
    }
}
