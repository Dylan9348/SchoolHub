
using SchoolHubApi.Models.WorkModels;

namespace SchoolHubApi.Repositories;

public interface IWorkRepository
{
    Task<Work?> GetWorkAsync(Guid id, CancellationToken ct = default);
    Task<bool> WorkExistAsync(Guid id, CancellationToken ct = default);

    Task<Guid> CreateInstructionWorkAsync(Guid creatorId, string instructions, string title, uint maxValue = 0, bool isHomework = false, DateOnly? date = null);

    Task<Guid> CreateQuizWorkAsync(Guid creatorId, Dictionary<string, string?> questions, string title, uint maxValue = 0, bool isHomework = false, DateOnly? date = null);
}
