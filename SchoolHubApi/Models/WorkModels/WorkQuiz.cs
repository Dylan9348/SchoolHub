namespace SchoolHubApi.Models.WorkModels;

public class WorkQuiz : Work
{
    public Dictionary<string, string> Questions { get; set; } = [];
}
