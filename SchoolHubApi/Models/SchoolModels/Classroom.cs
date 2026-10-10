namespace SchoolHubApi.Models.SchoolModels;

public class Classroom
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public ICollection<Student> Students { get; set; } = [];
    public double AverageGrade { get; set; }
}
