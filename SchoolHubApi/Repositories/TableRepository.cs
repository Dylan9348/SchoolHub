
using SchoolHubApi.Models.TableModels;
using SchoolHubApi.Data;
using SchoolHubApi.Models.UserModels;
using SchoolHubApi.Models.SchoolModels.Permissions;
using Microsoft.EntityFrameworkCore;

namespace SchoolHubApi.Repositories;

public class TableRepository(
    Context database
) : ITableRepository
{
    private readonly Context _database = database;

    public async Task<Table?> GetTableByIdAsync(Guid id, CancellationToken ct = default)
        => await _database.Tables
            .AsNoTracking()
            .SingleOrDefaultAsync(t => t.Id == id, ct);

    public async Task<bool> OwnerHasTableNamedAsync(Guid ownerId, string tableName)
        => await _database.Tables.AnyAsync(t => t.OwnerId == ownerId && t.Name == tableName);

    public async Task<Guid> CreateTableAsync(Guid userId, string tableName)
    {
        var table = new Table
        {
            Name = tableName,
            OwnerId = userId,
            Members =
            [
                new TableMember { UserId = userId, Role = TableRole.Owner }
            ]
        };

        _database.Add(table);
        
        await _database.SaveChangesAsync();

        return table.Id;
    }

    public async Task<Table?> GetTableAsync(string owner, string tableName) =>
        await _database.Tables
            .AsNoTracking()
            .Include(t => t.Owner)
            .SingleOrDefaultAsync(t => t.Owner.Username == owner && t.Name == tableName);
        
    public async Task<bool> IsMemberAsync(Guid userId, Guid tableId, CancellationToken ct = default) =>
        await _database.TableMembers
            .AsNoTracking()
            .AnyAsync(m => m.TableId == tableId && m.UserId == userId, ct);

    public async Task<TableRole?> GetMemberRoleAsync(Guid userId, Guid tableId, CancellationToken ct = default) =>
        await _database.TableMembers
            .AsNoTracking()
            .Where(m => m.TableId == tableId && m.UserId == userId)
            .Select(m => (TableRole?)m.Role)
            .FirstOrDefaultAsync(ct);

    public async Task ChangeRoleAsync(Guid memberId, TableRole newRole, CancellationToken ct = default)
    {
        var member = await _database.TableMembers
            .SingleOrDefaultAsync(m => m.Id == memberId, ct);

        if (member is null)
            return;

        member.Role = newRole;

        await _database.SaveChangesAsync(ct);
    }

    public async Task<bool> AddMemberAsync(Guid userId, Guid tableId, CancellationToken ct)
    {
        var tableExists = await _database.Tables.AnyAsync(t => t.Id == tableId, ct);
        var userExists = await _database.Users.AnyAsync(u => u.Id == userId, ct);
        var alreadyMember = await _database.TableMembers
            .AnyAsync(m => m.TableId == tableId && m.UserId == userId, ct);

        if (!tableExists || !userExists || alreadyMember)
            return false;
        
        var member = new TableMember
        {
            UserId = userId,
            TableId = tableId,
            Role = TableRole.Member
        };

        _database.TableMembers.Add(member);

        try
        {
            await _database.SaveChangesAsync(ct);
        }
        catch (DbUpdateException)
        {
            return false;
        }

        return true;
    }
}
