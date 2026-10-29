
using Microsoft.EntityFrameworkCore;
using SchoolHubApi.Data;
using SchoolHubApi.Models.SchoolModels.Permissions;

namespace SchoolHubApi.Repositories;

public class MemberRepository(Context database) : IMemberRepository
{
    private readonly Context _database = database;

    public async Task<TableRole?> GetRoleByIdAsync(Guid memberId, CancellationToken ct = default) =>
        await _database.TableMembers
            .AsNoTracking()
            .Where(m => m.Id == memberId)
            .Select(m => (TableRole?)m.Role)
            .FirstOrDefaultAsync(ct);
}
