namespace WFM.UxModels.Models;

public class BranchUxModel
{
    public int Id { get; set; }

    public string BranchName { get; set; } = null!;

    public int CountryId { get; set; }
}