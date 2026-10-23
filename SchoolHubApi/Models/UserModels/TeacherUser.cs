using SchoolHubApi.Models.SchoolModels;
using SchoolHubApi.Models.WorkModels;

namespace SchoolHubApi.Models.UserModels;

public class TeacherUser : SchoolHubUser
{
    public Teacher? Self { get; set; }
    public ICollection<Classroom> Classrooms { get; set; } = [];
    public ICollection<Work> Works { get; set; } = [];
}
