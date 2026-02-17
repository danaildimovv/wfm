namespace WFM.Database.Models;

public partial class Payroll : Root<int>
{
    public int HourlyRate { get; set; }

    public int GrossSalary { get; set; }

    public int NetSalary { get; set; }

    public DateOnly EffectiveDate { get; set; }

    public DateOnly LastChanged { get; set; }

    public virtual Employee? Employee { get; set; }
}
