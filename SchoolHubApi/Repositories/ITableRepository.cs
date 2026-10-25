
using SchoolHubApi.Models.SchoolModels.Permissions;
using SchoolHubApi.Models.TableModels;
using SchoolHubApi.Models.UserModels;

namespace SchoolHubApi.Repositories;

public interface ITableRepository
{
    Task<Table?> GetTableByIdAsync(Guid id, CancellationToken ct = default);
    Task<bool> OwnerHasTableNamedAsync(Guid ownerId, string tableName);
    Task<Guid> CreateTableAsync(Guid userId, string tableName);
    Task<Table?> GetTableAsync(string owner, string tableName);
    Task<bool> IsMemberAsync(Guid userId, Guid tableId, CancellationToken ct = default);
    Task<TableRole?> GetMemberRoleAsync(Guid userId, Guid tableId, CancellationToken ct = default);
    Task<bool> AddMemberAsync(Guid userId, Guid tableId, CancellationToken ct);
}
