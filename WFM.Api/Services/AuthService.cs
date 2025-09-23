using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using WFM.Api.Services.Interfaces;
using WFM.Database.Models;
using WFM.Database.Repositories.Interfaces;
using WFM.UxModels.Models;

namespace WFM.Api.Services;

public class AuthService(IConfiguration configuration, IRoleService roleService, IUserRepository repository) : IAuthService
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 10000;

    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;
    
    public async Task<bool> RegisterAsync(UserUxModel model)
    {
        var user = await repository.GetByUsernameAsync(model.Username);
        if (user is not null)
        {
            return false;
        }
        
        var passwordSalt = RandomNumberGenerator.GetBytes(SaltSize);
        var hashedPassword = Rfc2898DeriveBytes
            .Pbkdf2(model.Password, passwordSalt, Iterations, Algorithm, HashSize);

        var newUser = new User
        {
            Username = model.Username,
            PasswordHash = hashedPassword,
            PasswordSalt = passwordSalt
        };
        
        return await repository.AddAsync(newUser);
    }
    
    public async Task<string?> LoginAsync(UserUxModel model)
    {
        var user = await repository.GetByUsernameAsync(model.Username);
        

        if (user is null)
        {
            return null;
        }
        
        var inputHash = Rfc2898DeriveBytes.Pbkdf2(model.Password, user.PasswordSalt, Iterations, Algorithm, HashSize);
        var isPasswordValid = CryptographicOperations.FixedTimeEquals(inputHash, user.PasswordHash);

        if (!isPasswordValid)
        {
            return null;
        }
        
        var token = CreateToken(user);
        
        return token;
    }
    
    private string CreateToken(User user)
    {   
        var claims = new Dictionary<string, object>
        {
            [ClaimTypes.Name] = user.Username,
            [ClaimTypes.NameIdentifier] = user.UserId.ToString(),
            [ClaimTypes.Role] = user.Role.RoleTitle,
        };
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration.GetValue<string>("KEY")!));
        
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = configuration.GetValue<string>("AppSettings:TokenIssuer"),
            Audience = configuration.GetValue<string>("AppSettings:TokenAudience"),
            Claims = claims,
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = credentials
        };

        return new JsonWebTokenHandler().CreateToken(tokenDescriptor);
    }
    
}
