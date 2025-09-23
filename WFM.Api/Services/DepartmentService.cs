using WFM.Api.Services.Interfaces;
using WFM.Database.Models;
using WFM.Database.Repositories.Interfaces;

namespace WFM.Api.Services;

public class DepartmentService(IDepartmentRepository repository) 
    : BaseService<Department>(repository), IDepartmentService;
