namespace WFM.UxModels.Models;

public class EmployeesJobHistoryUxModel
{
    public int EmployeeJobId { get; set; }

    public int EmployeeId { get; set; }

    public int JobId { get; set; }

    public DateOnly DateStarted { get; set; }

    public DateOnly? DateEnded { get; set; }
}