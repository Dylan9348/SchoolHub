
using FluentResults;
using Microsoft.EntityFrameworkCore;
using SchoolHubApi.Data;
using SchoolHubApi.Models.TableModels;
using SchoolHubApi.Results;

namespace SchoolHubApi.Repositories;

public class MessageRepository(Context database) : IMessageRepository
{
    private readonly Context _database = database;

    public async Task<Guid> CreateMessageAsync(Guid senderId, Guid categoryId, Guid workId, bool pending, CancellationToken ct = default)
    {
        var message = new TableMessage
        {
            Date = DateOnly.FromDateTime(DateTime.UtcNow),
            WorkId = workId,
            CategoryId = categoryId,
            SenderId = senderId,
            Pending = pending
        };

        _database.Add(message);

        await _database.SaveChangesAsync(ct);

        return message.Id;
    }

    public async Task<ICollection<TableMessage>?> GetMessagesAsync(Guid categoryId, ushort pageSize, ushort page, DateOnly? afterTo = null, DateOnly? beforeTo = null, DateOnly? workDate = null, string? title = null, string? sendedBy = null, bool? isPending = null, CancellationToken ct = default)
    {
        var skip = pageSize * page;

        var category = await _database.TableCategories
            .AsNoTracking()
            .Include(c => c.Messages
                .Where(m =>
                    (afterTo == null || m.Date > afterTo) &&
                    (beforeTo == null || m.Date < beforeTo) &&
                    (workDate == null || m.Work.Date == workDate) &&
                    (title == null || m.Work.Title == title) &&
                    (sendedBy == null || m.Sender.Username == sendedBy) &&
                    (isPending == null || m.Pending == isPending))
                .OrderBy(m => m.Date)
                .Skip(skip)
                .Take(pageSize))
            .SingleOrDefaultAsync(c => c.Id == categoryId, ct);


        return category?.Messages;
    }

    public async Task<bool> MessageExistAsync(Guid id, CancellationToken ct = default) =>
        await _database.Set<TableMessage>().AnyAsync(m => m.Id == id, ct);

    public async Task<Guid?> GetMessageTableIdAsync(Guid id, CancellationToken ct = default) =>
        await _database.Set<TableMessage>()
            .AsNoTracking()
            .Where(m => m.Id == id)
            .Select(m => (Guid?)m.Category.TableId)
            .FirstOrDefaultAsync(ct);

    public async Task<Result> AproveMessageAsync(Guid messageId)
    {
        var rows = await _database.TableMessages
            .Where(m => m.Id == messageId)
            .ExecuteUpdateAsync(s => s.SetProperty(m => m.Pending, false));

        return rows == 0 
            ? Result.Fail(new NotFoundError("messageId", "message not found")) 
            : Result.Ok();
    }
}
