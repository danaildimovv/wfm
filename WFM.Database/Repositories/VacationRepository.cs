using Microsoft.EntityFrameworkCore;
using WFM.Database.DbContext;
using WFM.Database.Models;
using WFM.Database.Repositories.Interfaces;

namespace WFM.Database.Repositories;

public class VacationRepository(WfmContext dbContext)
    : BaseRepository<Vacation>(dbContext), IVacationRepository
{
    private readonly DbSet<Vacation> _entity = dbContext.Set<Vacation>();
    
    public async Task<IEnumerable<Vacation>> GetVacationsByEmployeeIdAsync(long employeeId)
    {
        return await _entity
            .Where(x => x.EmployeeId == employeeId)
            .ToListAsync();
    }
}
