
using SchoolHubApi.Models.TableModels;
using SchoolHubApi.Data;
using SchoolHubApi.Models.UserModels;
using SchoolHubApi.Models.SchoolModels.Permissions;
using Microsoft.EntityFrameworkCore;

namespace SchoolHubApi.Repositories;

public class TableRepository(Context database) : ITableRepository
{
    private readonly Context _database = database;

    public async Task<Table?> GetTableByIdAsync(Guid id)
        => await _database.Tables.FindAsync(id);

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
}
