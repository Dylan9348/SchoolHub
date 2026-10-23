
using SchoolHubApi.Models.WorkModels;

namespace SchoolHubApi.Repositories;

public interface IWorkRepository
{
    Task<Work?> GetWorkAsync(Guid id);

    Task<Guid> CreateInstructionWorkAsync(string instructions, string title, int maxValue = 0, bool isHomework = false, DateOnly? date = null);

    Task<Guid> CreateQuizWorkAsync(Dictionary<string, string?> questions, string title, int maxValue = 0, bool isHomework = false, DateOnly? date = null);
}
