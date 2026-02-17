using WFM.Api.Enums;

namespace WFM.Database.Models;

public partial class User : Root<int>
{
    public string Username { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public int RoleId { get; set; } = (int)RoleEnum.RegularUser;

    public byte[] PasswordSalt { get; set; } = null!;

    public virtual Employee? Employee { get; set; }

    public virtual Role Role { get; set; } = null!;
}
