namespace WFM.UxModels.Models;

public class EmployeesBranchesHistoryUxModel
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public int BranchId { get; set; }

    public DateOnly DateStarted { get; set; }

    public DateOnly? DateEnded { get; set; }
}