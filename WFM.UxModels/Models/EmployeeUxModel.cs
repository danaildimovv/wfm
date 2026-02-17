namespace WFM.UxModels.Models;

public class EmployeeUxModel
{
    public int Id { get; set; }

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
}