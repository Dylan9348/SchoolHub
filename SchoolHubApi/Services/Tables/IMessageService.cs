
using FluentResults;

namespace SchoolHubApi.Services.Tables;

public interface IMessageService
{
    Task<Result> SendMessageAsync(Guid userId, Guid workId, Guid categoryId);
}
