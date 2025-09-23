using WFM.Database.DbContext;
using WFM.Database.Models;
using WFM.Database.Repositories.Interfaces;

namespace WFM.Database.Repositories;

public class EmployeesJobHistoryRepository(WfmContext dbContext)
    : BaseRepository<EmployeesJobHistory>(dbContext), IEmployeesJobHistoryRepository;
