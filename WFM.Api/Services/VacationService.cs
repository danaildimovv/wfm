using WFM.Api.Services.Interfaces;
using WFM.Database.Models;
using WFM.Database.Repositories.Interfaces;

namespace WFM.Api.Services;

public class VacationService(IVacationRepository repository)
    : BaseService<Vacation>(repository), IVacationService
{
    public async Task<IEnumerable<Vacation>> GetVacationsByEmployeeIdAsync(long employeeId)
    {
        return await repository.GetVacationsByEmployeeIdAsync(employeeId);
    }
}
