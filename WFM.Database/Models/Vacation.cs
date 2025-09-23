namespace WFM.Database.Models;

public partial class Vacation
{
    public int VacationId { get; set; }

    public int EmployeeId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string Reason { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
