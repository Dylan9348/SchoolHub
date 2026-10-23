
using SchoolHubApi.Models.SchoolModels;
using SchoolHubApi.Models.TableModels;

namespace SchoolHubApi.Models.UserModels;

public class SchoolHubUser
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string Email { get; set; } = "";
    public ICollection<School> Schools { get; set; } = [];
    public ICollection<TableMember> Memberships { get; set; } = [];
    public ICollection<Table> OwnTables { get; set; } = [];
    public ICollection<TableMessage> TableMessages { get; set; } = [];
}
