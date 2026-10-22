
using FluentResults;
using SchoolHubApi.DTOs;
using SchoolHubApi.Repositories;
using SchoolHubApi.Validators;

namespace SchoolHubApi.Services.Tables;

public class CategoryService(
    ICategoryRepository categoryRepository,
    CategoryValidator categoryValidator) : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly CategoryValidator _categoryValidator = categoryValidator;

    public async Task<Result> CreateCategoryAsync(CreateCategoryDto category)
    {
        var validationResult = await _categoryValidator.ValidateCategoryCreation(category.TableId, category.CategoryName);

        if (validationResult.IsFailed)
            return validationResult;

        await _categoryRepository.CreateCategoryAsync(category.TableId, category.CategoryName);

        return Result.Ok();
    }
}
