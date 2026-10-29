
using SchoolHubApi.Models.SchoolModels.Permissions;

namespace SchoolHubApi.Repositories;

public interface IMemberRepository
{
    Task<TableRole?> GetRoleByIdAsync(Guid memberId, CancellationToken ct = default);
}
