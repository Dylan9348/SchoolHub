
using SchoolHubApi.Enums;
using SchoolHubApi.Repositories;
using SchoolHubApi.Validators.ValidationResults;

namespace SchoolHubApi.Validators;

public class TableValidator(
    ITableRepository tableRepository,
    IUserRepository userRepository
)
{
    private readonly ITableRepository _tableRepository = tableRepository;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ValidationResult> ValidateTableCreationAsync(Guid userId, string tableName)
    {
        if (tableName.Length < 2)
            return new FailedValidationResult(FailedValidationType.TooShort, tableName, "tableName too short");

        if (tableName.Length > 50)
            return new FailedValidationResult(FailedValidationType.TooLong, tableName, "tableName is too long");

        if (await _tableRepository.OwnerHasTableNamedAsync(userId, tableName))
            return new FailedValidationResult(FailedValidationType.Conflict, tableName, "You already have a table with that name.");

        return new PassedValidationResult();
    }

    public async Task<ValidationResult> ValidateTableAddMemberAsync(Guid userId, Guid tableId, CancellationToken ct = default)
    {
        if (!await _userRepository.UserExistAsync(userId, ct))
            return new FailedValidationResult(FailedValidationType.NotFound, userId, "User not found.");

        if (await _tableRepository.GetTableByIdAsync(tableId, ct) is null)
            return new FailedValidationResult(FailedValidationType.NotFound, tableId, "Table not found.");

        if (await _tableRepository.IsMemberAsync(userId, tableId, ct))
            return new FailedValidationResult(FailedValidationType.Conflict, userId, "User is already a member of the table.");

        return new PassedValidationResult();
    }
}
