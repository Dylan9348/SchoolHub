
using SchoolHubApi.Models.TableModels;
using SchoolHubApi.Models.UserModels;

namespace SchoolHubApi.Repositories;

public interface ITableRepository
{
    Task<Table?> GetTableByIdAsync(Guid id);
    Task<bool> OwnerHasTableNamedAsync(Guid ownerId, string tableName);
    Task<Guid> CreateTableAsync(Guid userId, string tableName);
}
