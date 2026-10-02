namespace SchoolHubApi.Models.SchoolModels;

public class School
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public ICollection<Classroom> Classrooms = [];
    public ICollection<Teacher> Teachers = [];
}
