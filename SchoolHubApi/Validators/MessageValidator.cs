
using FluentResults;
using SchoolHubApi.Repositories;
using SchoolHubApi.Results;

namespace SchoolHubApi.Validators;

public class MessageValidator(
    IUserRepository userRepository,
    ICategoryRepository categoryRepository,
    IWorkRepository workRepository,
    ITableRepository tableRepository
)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IWorkRepository _workRepository = workRepository;
    private readonly ITableRepository _tableRepository = tableRepository;

    public async Task<Result> ValidateSendMessageAsync(Guid userId, Guid categoryId, Guid workId, CancellationToken ct = default)
    {
        if (!await _userRepository.UserExistAsync(userId, ct))
            return Result.Fail(new NotFoundError("userId", "User not found."));

        var category = await _categoryRepository.GetCategoryAsync(categoryId, ct);

        if (category is null)
            return Result.Fail(new NotFoundError("categoryId", "Category not found."));

        if (!await _workRepository.WorkExistAsync(workId, ct))
            return Result.Fail(new NotFoundError("workId", "Work not found."));

        if (!await _tableRepository.IsMemberAsync(userId, category.TableId, ct))
            return Result.Fail(new ForbiddenError("userId", "User is not a member of the category's table."));

        return Result.Ok();
    }
}
