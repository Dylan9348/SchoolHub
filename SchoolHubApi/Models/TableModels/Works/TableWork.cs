
namespace SchoolHubApi.Models.TableModels.Works;

public abstract class TableWork
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateOnly? Date { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public string Title { get; set; } = "";
}