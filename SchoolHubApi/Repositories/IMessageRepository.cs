
namespace SchoolHubApi.Repositories;

public interface IMessageRepository
{
    Task<Guid> CreateMessageAsync(Guid senderId, Guid categoryId, Guid workId, bool pending, CancellationToken ct = default);
}
