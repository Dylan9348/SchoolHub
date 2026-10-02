
using SchoolHubApi.Models.UserModels;

namespace SchoolHubApi.Models.SchoolModels;

public class Table
{
    public Guid Id { get; set; }= Guid.NewGuid();
    public ICollection<Work> Works { get; set; } = [];
    public ICollection<StudentUser> Members { get; set; } = [];
    public Dictionary<string, string> Permissions { get; set; } = []; // <username, permission>
}