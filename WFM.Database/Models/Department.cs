namespace WFM.Database.Models;

public partial class Department : Root<int>
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}
