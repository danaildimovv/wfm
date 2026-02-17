using WFM.Api.Services.Interfaces;
using WFM.Database.Models;
using WFM.Database.Repositories.Interfaces;

namespace WFM.Api.Services;

public class PayrollService(IPayrollRepository repository)
    : BaseService<Payroll>(repository), IPayrollService
{
    public async Task<Payroll> GetByEmployeeIdAsync(int employeeId)
    {
        var entity = await repository.GetByEmployeeIdAsync(employeeId);
        ArgumentNullException.ThrowIfNull(entity);
        
        return entity;
    }
}
