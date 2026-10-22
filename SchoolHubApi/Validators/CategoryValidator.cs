
using FluentResults;
using SchoolHubApi.Repositories;
using SchoolHubApi.Results;

namespace SchoolHubApi.Validators;

public class CategoryValidator(ICategoryRepository categoryRepository)
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<Result> ValidateCategoryCreation(Guid tableId, string categoryName)
    {
        var nameAlreadyTaken = await _categoryRepository.CategoryExist(tableId, categoryName);
        
        if (nameAlreadyTaken)
            return Result.Fail(new AlreadyTakenError("name", $"A category with the name '{categoryName}' already exists in that table"));

        return Result.Ok();
    }
}
