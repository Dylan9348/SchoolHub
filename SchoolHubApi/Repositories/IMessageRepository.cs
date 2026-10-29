
using FluentResults;
using SchoolHubApi.Models.TableModels;

namespace SchoolHubApi.Repositories;

public interface IMessageRepository
{
    Task<Guid> CreateMessageAsync(Guid senderId, Guid categoryId, Guid workId, bool pending, CancellationToken ct = default);
    Task<ICollection<TableMessage>?> GetMessagesAsync(Guid categoryId, ushort pageSize, ushort page, DateOnly? afterTo = null, DateOnly? beforeTo = null, DateOnly? workDate = null, string? title = null, string? sendedBy = null, bool? isPending = null, CancellationToken ct = default);
    Task<bool> MessageExistAsync(Guid id, CancellationToken ct = default);
    Task<Guid?> GetMessageTableIdAsync(Guid id, CancellationToken ct = default);
    Task<Result> AproveMessageAsync(Guid messageId);
}
