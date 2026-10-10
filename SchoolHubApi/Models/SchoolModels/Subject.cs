namespace SchoolHubApi.Models.SchoolModels;

public class Subject
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Teacher Teacher { get; set; } = new();
    public Classroom Classroom { get; set; } = new();
    public ICollection<Work> Works { get; set; } = [];
    public double AverageGrade { get; set; }
}
