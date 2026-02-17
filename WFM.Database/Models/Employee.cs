namespace WFM.Database.Models;

public partial class Employee : Root<int>
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int JobId { get; set; }

    public int ExperienceLevelId { get; set; }

    public int PayrollId { get; set; }

    public int BranchId { get; set; }

    public string Email { get; set; } = null!;

    public int UserId { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string EmployeeAddress { get; set; } = null!;

    public int NationalityId { get; set; }

    public string Gender { get; set; } = null!;

    public DateOnly DateOfEmployment { get; set; }

    public DateOnly? DateOfLeaving { get; set; }

    public int RemainingVacationDays { get; set; }

    public int UsedVacationDays { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual ICollection<EmployeesBranchesHistory> EmployeesBranchesHistories { get; set; } = new List<EmployeesBranchesHistory>();

    public virtual ICollection<EmployeesJobHistory> EmployeesJobHistories { get; set; } = new List<EmployeesJobHistory>();

    public virtual ExperienceLevel ExperienceLevel { get; set; } = null!;

    public virtual Job Job { get; set; } = null!;

    public virtual Country Nationality { get; set; } = null!;

    public virtual Payroll Payroll { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Vacation> Vacations { get; set; } = new List<Vacation>();
}
