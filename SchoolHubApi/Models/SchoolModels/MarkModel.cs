namespace SchoolHubApi.Models.SchoolModels;

public class Mark
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Student Student { get; set; } = new();
    public Subject Subject { get; set; } = new();
    public ICollection<Guid> FinishedWorksIds = [];
}
