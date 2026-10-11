
using SchoolHubApi.Models.SchoolModels.Permissions;
using SchoolHubApi.Models.UserModels;

namespace SchoolHubApi.Models.TableModels;

public class TableMember
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public SchoolHubUser User { get; set; } = new();
    public TableRole Role { get; set; } = TableRole.Member;
}
