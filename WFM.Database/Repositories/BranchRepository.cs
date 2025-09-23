using WFM.Database.DbContext;
using WFM.Database.Models;
using WFM.Database.Repositories.Interfaces;

namespace WFM.Database.Repositories;

public class BranchRepository(WfmContext dbContext) 
    : BaseRepository<Branch>(dbContext), IBranchRepository;