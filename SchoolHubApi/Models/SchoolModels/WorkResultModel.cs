namespace SchoolHubApi.Models.SchoolModels;

public class WorkResult
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Work Work { get; set; } = new();
    public int MaxScore { get; set; }
    public int ActualScore { get; set; }
}
