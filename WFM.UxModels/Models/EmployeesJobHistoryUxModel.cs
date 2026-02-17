namespace WFM.UxModels.Models;

public class EmployeesJobHistoryUxModel
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public int JobId { get; set; }

    public DateOnly DateStarted { get; set; }

    public DateOnly? DateEnded { get; set; }
}