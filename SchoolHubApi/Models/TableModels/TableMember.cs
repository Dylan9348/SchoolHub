
using SchoolHubApi.Models.SchoolModels.Permissions;
using SchoolHubApi.Models.UserModels;

namespace SchoolHubApi.Models.TableModels;

public class TableMember
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TableId { get; set; }
    public Table Table { get; set; } = null!;
    public Guid UserId { get; set; }
    public SchoolHubUser User { get; set; } = null!;
    public TableRole Role { get; set; } = TableRole.Member;
}
