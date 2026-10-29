
using FluentResults;
using SchoolHubApi.Models.SchoolModels.Permissions;
using SchoolHubApi.Repositories;
using SchoolHubApi.Validators;

namespace SchoolHubApi.Services.Tables;

public class TableService(
    TableValidator tableValidator,
    ITableRepository tableRepository
) : ITableService
{
    private readonly TableValidator _tableValidator = tableValidator;
    private readonly ITableRepository _tableRepository = tableRepository;

    public async Task<Result> PromoteToAdminAsync(Guid memberId)
    {
        var validationResult = await _tableValidator.ValidateRoleAssignmentAsync(memberId);

        if (validationResult.IsFailed)
            return validationResult;

        await _tableRepository.ChangeRoleAsync(memberId, TableRole.Admin);

        return Result.Ok();
    }
}
