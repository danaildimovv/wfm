using Microsoft.EntityFrameworkCore;
using WFM.Database.DbContext;
using WFM.Database.Models;
using WFM.Database.Repositories.Interfaces;

namespace WFM.Database.Repositories;

public class UserRepository(WfmContext dbContext)
    : BaseRepository<User>(dbContext), IUserRepository
{
    private readonly DbSet<User> _entity = dbContext.Set<User>();

    public async Task<User?> GetByUsernameAsync(string username)
    {
        var user = await _entity.FirstOrDefaultAsync(x => x.Username == username);
        if (user is not null)
        {
            await _entity.Entry(user).Reference(s => s.Role).LoadAsync();
        }

        return user;
    }
}
