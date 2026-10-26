
using FluentResults;
using SchoolHubApi.Repositories;
using SchoolHubApi.Results;

namespace SchoolHubApi.Validators;

public class CategoryValidator(
    ICategoryRepository categoryRepository,
    IUserRepository userRepository,
    ITableRepository tableRepository
)
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITableRepository _tableRepository = tableRepository;

    public async Task<Result> ValidateCategoryCreation(Guid userId, Guid tableId, string categoryName)
    {
        if (!await _userRepository.UserExistAsync(userId))
            return Result.Fail(new UnauthorizedError("userId", "User not found."));

        var table = await _tableRepository.GetTableByIdAsync(tableId);

        if (table is null)
            return Result.Fail(new NotFoundError("tableId", "Table not found."));

        if (table.OwnerId != userId)
            return Result.Fail(new ForbiddenError("userId", "User is not the owner of the table."));

        var nameAlreadyTaken = await _categoryRepository.CategoryExist(tableId, categoryName);

        if (nameAlreadyTaken)
            return Result.Fail(new AlreadyTakenError("name", $"A category with the name '{categoryName}' already exists in that table"));

        return Result.Ok();
    }
}
