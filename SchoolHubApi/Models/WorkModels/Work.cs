
using SchoolHubApi.Models.UserModels;

namespace SchoolHubApi.Models.WorkModels;

public abstract class Work
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateOnly Date { get; set; }
    public uint MaxValue { get; set; }
    public string Title { get; set; } = "";
    public bool IsHomework { get; set; }
    public Guid CreatorId { get; set; }
    public SchoolHubUser Creator { get; set; } = null!;
}
