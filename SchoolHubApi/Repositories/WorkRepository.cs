
using Microsoft.EntityFrameworkCore;
using SchoolHubApi.Data;
using SchoolHubApi.Models.WorkModels;

namespace SchoolHubApi.Repositories;

public class WorkRepository(Context database) : IWorkRepository
{
    private readonly Context _database = database;

    public async Task<Guid> CreateInstructionWorkAsync(Guid creatorId, string instructions, string title, uint maxValue = 0, bool isHomework = false, DateOnly? date = null)
    {
        var work = new WorkInstruction
        {
            Date = date ?? DateOnly.FromDateTime(DateTime.UtcNow),
            Instruction = instructions,
            Title = title,
            MaxValue = maxValue,
            IsHomework = isHomework,
            CreatorId = creatorId,
        };

        _database.Add(work);

        await _database.SaveChangesAsync();

        return work.Id;
    }

    public async Task<Guid> CreateQuizWorkAsync(Guid creatorId, Dictionary<string, string?> questions, string title, uint maxValue = 0, bool isHomework = false, DateOnly? date = null)
    {
        var work = new WorkQuiz
        {
            Date = date ?? DateOnly.FromDateTime(DateTime.UtcNow),
            Title = title,
            MaxValue = maxValue,
            IsHomework = isHomework,
            Questions = questions,
            CreatorId = creatorId,
        };

        _database.Add(work);

        await _database.SaveChangesAsync();

        return work.Id;
    }

    public async Task<Work?> GetWorkAsync(Guid id, CancellationToken ct = default) =>
        await _database.Works
            .AsNoTracking()
            .FirstOrDefaultAsync(w => w.Id == id, ct);

    public async Task<bool> WorkExistAsync(Guid id, CancellationToken ct = default) =>
        await _database.Works.AnyAsync(w => w.Id == id, ct);
}
