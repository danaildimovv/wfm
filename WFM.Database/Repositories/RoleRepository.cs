using WFM.Database.DbContext;
using WFM.Database.Models;
using WFM.Database.Repositories.Interfaces;

namespace WFM.Database.Repositories;

public class RoleRepository(WfmContext dbContext) 
    : BaseRepository<Role>(dbContext), IRoleRepository;
