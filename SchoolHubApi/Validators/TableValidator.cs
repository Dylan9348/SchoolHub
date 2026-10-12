
using SchoolHubApi.Repositories;
using SchoolHubApi.Validators.ValidationResult;

namespace SchoolHubApi.Validators;

public class TableValidator(ITableRepository tableRepository)
{
    private readonly ITableRepository _tableRepository = tableRepository;

    public async Task<ValidationResult<object?>> ValidateTableCreationAsync(Guid userId, string tableName)
    {
        if (tableName.Length < 2)
            return new FailedValidationResult<object?, string>(tableName, tableName, "too short");

        if (tableName.Length > 50)
            return new FailedValidationResult<object?, string>(tableName, tableName, "too long");

        if (await _tableRepository.OwnerHasTableNamedAsync(userId, tableName))
            return new FailedValidationResult<object?, string>(tableName, tableName, "You already have a table with that name.");

        return new PassedValidationResult<object?>(tableName);
    }
}
