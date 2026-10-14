
using SchoolHubApi.Enums;
using SchoolHubApi.Repositories;
using SchoolHubApi.Validators.ValidationResults;

namespace SchoolHubApi.Validators;

public class TableValidator(ITableRepository tableRepository)
{
    private readonly ITableRepository _tableRepository = tableRepository;

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
}
