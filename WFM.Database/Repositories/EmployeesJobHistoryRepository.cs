using Microsoft.EntityFrameworkCore;
using WFM.Database.DbContext;
using WFM.Database.Models;
using WFM.Database.Repositories.Interfaces;

namespace WFM.Database.Repositories;

public class EmployeesJobHistoryRepository(WfmContext dbContext)
    : BaseRepository<EmployeesJobHistory>(dbContext), IEmployeesJobHistoryRepository
{
    private readonly DbSet<EmployeesJobHistory> _entity = dbContext.Set<EmployeesJobHistory>();
    
    public async Task<List<EmployeesJobHistory>> GetJobsByEmployeeIdAsync(int employeeId)
    {
        return await _entity
            .Where(x => x.EmployeeId == employeeId)
            .OrderByDescending(x => x.DateStarted)
            .ToListAsync();
    }
    
    public async Task<List<EmployeesJobHistory>> GetEmployeesByJobIdAsync(int jobId)
    {
        return await _entity
            .Where(x => x.JobId == jobId)
            .ToListAsync();
    }
}
