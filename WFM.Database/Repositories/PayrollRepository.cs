using Microsoft.EntityFrameworkCore;
using WFM.Database.DbContext;
using WFM.Database.Models;
using WFM.Database.Repositories.Interfaces;

namespace WFM.Database.Repositories;

public class PayrollRepository(WfmContext dbContext)
    : BaseRepository<Payroll>(dbContext), IPayrollRepository
{
    private readonly DbSet<Payroll> _entity = dbContext.Set<Payroll>();

    public async Task<Payroll?> GetByEmployeeIdAsync(int employeeId)
    {
        return await _entity.Where(x => x.Employee != null && x.Employee.Id == employeeId).SingleOrDefaultAsync();
    }
}
