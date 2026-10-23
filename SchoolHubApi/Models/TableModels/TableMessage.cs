
using SchoolHubApi.Models.UserModels;
using SchoolHubApi.Models.WorkModels;

namespace SchoolHubApi.Models.TableModels;

public class TableMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateOnly Date { get; set; }
    public Guid WorkId { get; set; }
    public Work Work { get; set; } = null!;
    public Guid TableId { get; set; }
    public Table Table { get; set; } = null!;
    public bool Pending { get; set; } = true;
    public Guid SenderId { get; set; }
    public SchoolHubUser Sender { get; set; } = null!;
}
