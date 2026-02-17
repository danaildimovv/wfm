namespace WFM.Database.Models;

public partial class Role : Root<int>
{
    public string RoleTitle { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
