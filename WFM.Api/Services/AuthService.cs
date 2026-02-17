using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using WFM.Api.Exceptions;
using WFM.Api.Services.Interfaces;
using WFM.Database.Models;
using WFM.Database.Repositories.Interfaces;
using WFM.UxModels.Models;
using AuthenticationException = System.Security.Authentication.AuthenticationException;

namespace WFM.Api.Services;

public class AuthService(IConfiguration configuration, IUserRepository repository) : IAuthService
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
            throw new AlreadyExistsException("Username already exists");
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
    
    public async Task<string> LoginAsync(UserUxModel model)
    {
        var user = await repository.GetByUsernameAsync(model.Username);

        if (user is null || !VerifyPassword(model.Password, user.PasswordSalt, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }
        
        var token = CreateToken(user);
        
        return token;
    }

    private static bool VerifyPassword(string password, byte[] passwordSalt, byte[] passwordHash)
    {
        var inputHash = Rfc2898DeriveBytes.Pbkdf2(password, passwordSalt, Iterations, Algorithm, HashSize);
        var isPasswordValid = CryptographicOperations.FixedTimeEquals(inputHash, passwordHash);
        
        return isPasswordValid;
    }
    
    private string CreateToken(User user)
    {   
        var claims = new Dictionary<string, object>
        {
            ["username"] = user.Username,
            ["userId"] = user.Id.ToString(),
            ["role"] = user.Role.RoleTitle,
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
