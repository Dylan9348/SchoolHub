
using SchoolHubApi.Data;
using SchoolHubApi.Models.TableModels;

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
}
