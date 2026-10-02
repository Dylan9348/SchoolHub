using SchoolHubApi.Models.SchoolModels;

namespace SchoolHubApi.Models.UserModels;

public class StudentUser : SchoolHubUser
{
    public new string? Email { get; set; } = "";
    public Student? Self { get; set; }
    public ICollection<Classroom> Classrooms { get; set; } = [];
}
