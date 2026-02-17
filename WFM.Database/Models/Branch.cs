namespace WFM.Database.Models;

public partial class Branch : Root<int>
{
    public string BranchName { get; set; } = null!;

    public int CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<EmployeesBranchesHistory> EmployeesBranchesHistories { get; set; } = new List<EmployeesBranchesHistory>();
}
