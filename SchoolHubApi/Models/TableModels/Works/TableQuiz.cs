
namespace SchoolHubApi.Models.TableModels.Works;

public class TableQuiz : TableWork
{
    public Dictionary<string, string?> Questions { get; set; }= []; // question / answer
}
