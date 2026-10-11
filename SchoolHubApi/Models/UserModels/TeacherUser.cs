using SchoolHubApi.Models.SchoolModels;

namespace SchoolHubApi.Models.UserModels;

public class TeacherUser : SchoolHubUser
{
    public Teacher? Self { get; set; }
    public ICollection<Classroom> Classrooms { get; set; } = [];
    public ICollection<SchoolWork> Works { get; set; } = [];
}
