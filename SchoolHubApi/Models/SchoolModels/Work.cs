namespace SchoolHubApi.Models.SchoolModels;

public class Work
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsHomework { get; set; } = false;
    public string? Title { get; set; }
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public int Value { get; set; } = 10;
}
