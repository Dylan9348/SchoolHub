
using SchoolHubApi.Models.TableModels.Works;
using SchoolHubApi.Models.UserModels;

namespace SchoolHubApi.Models.TableModels;

public class Table
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid OwnerId { get; set; }
    public SchoolHubUser Owner { get; set; } = null!;
    public ICollection<TableWork> Works { get; set; } = [];
    public ICollection<TableMember> Members { get; set; } = [];
}
