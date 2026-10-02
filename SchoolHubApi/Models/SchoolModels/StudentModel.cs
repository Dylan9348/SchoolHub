namespace SchoolHubApi.Models.SchoolModels;

public class Student
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Names { get; set; } = "";
    public string LastNames { get; set; } = "";
    public Classroom Classroom { get; set; } = new();
    public double AverageGrade { get; set; }
    public ICollection<Mark> Marks { get; set; } = [];
    public ICollection<StudentReport> Reports { get; set; } = [];
}
