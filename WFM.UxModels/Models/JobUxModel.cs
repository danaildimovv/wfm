namespace WFM.UxModels.Models;

public class JobUxModel
{
    public int JobId { get; set; }

    public string JobTitle { get; set; } = null!;

    public int DepartmentId { get; set; }
}