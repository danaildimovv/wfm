namespace WFM.Database.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public int RoleId { get; set; } = 2;

    public byte[] PasswordSalt { get; set; } = null!;

    public virtual Employee? Employee { get; set; }

    public virtual Role Role { get; set; } = null!;
}
