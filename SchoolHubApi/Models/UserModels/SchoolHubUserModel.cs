using SchoolHubApi.Models.SchoolModels;

namespace SchoolHubApi.Models.UserModels;

public class SchoolHubUser
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string Email { get; set; } = "";
    public ICollection<School> Schools = [];
}
