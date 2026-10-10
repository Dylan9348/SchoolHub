namespace SchoolHubApi.Models.SchoolModels;

public class TeacherReport
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Reason { get; set; } = "";
}
