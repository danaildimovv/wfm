using WFM.Database.Models;
using WFM.UxModels.Models;

namespace WFM.Api.Services.Interfaces;

public interface IAuthService
{
    public Task<bool> RegisterAsync(UserUxModel user);
    
    public Task<string?> LoginAsync(UserUxModel user);
}
