
using SchoolHubApi.Models.SchoolModels.Permissions;
using SchoolHubApi.Models.UserModels;

namespace SchoolHubApi.Models.SchoolModels;

public class Table
{
    public Guid Id { get; set; }= Guid.NewGuid();
    public ICollection<Work> Works { get; set; } = [];
    public ICollection<StudentUser> Members { get; set; } = [];
    public Dictionary<Guid, TablePermission> Permissions { get; set; } = [];
}
