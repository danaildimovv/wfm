namespace WFM.Database.Models;

public partial class ExperienceLevel : Root<int>
{
    public string ExperienceLevelTitle { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
