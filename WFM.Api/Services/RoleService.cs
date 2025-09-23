using WFM.Api.Services.Interfaces;
using WFM.Database.Models;
using WFM.Database.Repositories.Interfaces;

namespace WFM.Api.Services;

public class RoleService(IRoleRepository repository) 
    : BaseService<Role>(repository), IRoleService;
