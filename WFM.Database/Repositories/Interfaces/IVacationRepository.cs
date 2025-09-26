using WFM.Database.Models;

namespace WFM.Database.Repositories.Interfaces;

public interface IVacationRepository : IBaseRepository<Vacation>
{
    Task<IEnumerable<Vacation>> GetVacationsByEmployeeIdAsync(long employeeId);
}
