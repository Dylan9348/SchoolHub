
using FluentResults;

namespace SchoolHubApi.Services.Tables;

public interface ITableService
{
    Task<Result> PromoteToAdminAsync(Guid memberId);
}
