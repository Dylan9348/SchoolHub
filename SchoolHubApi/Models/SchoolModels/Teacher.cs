namespace SchoolHubApi.Models.SchoolModels;

public class Teacher
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Names { get; set; } = "";
    public string LastNames { get; set; } = "";
    public ICollection<Subject> Subjects { get; set; } = [];
    public ICollection<TeacherReport> Reports { get; set; } = [];
}
