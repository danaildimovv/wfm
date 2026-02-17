namespace WFM.Database.Models;

public partial class Country : Root<int>
{
    public string CountryName { get; set; } = null!;

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
