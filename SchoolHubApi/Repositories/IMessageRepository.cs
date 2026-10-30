
using FluentResults;
using SchoolHubApi.DTOs;
using SchoolHubApi.Models.TableModels;

namespace SchoolHubApi.Repositories;

public interface IMessageRepository
{
    Task<Guid> CreateMessageAsync(Guid senderId, Guid categoryId, Guid workId, bool pending, CancellationToken ct = default);
    Task<ICollection<TableMessage>?> GetMessagesAsync(GetMessagesQueryDto query, CancellationToken ct = default);
    Task<bool> MessageExistAsync(Guid id, CancellationToken ct = default);
    Task<Guid?> GetMessageTableIdAsync(Guid id, CancellationToken ct = default);
    Task<Result> AproveMessageAsync(Guid messageId);
}
