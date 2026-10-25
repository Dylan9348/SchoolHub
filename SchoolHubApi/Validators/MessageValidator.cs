
using FluentResults;
using SchoolHubApi.Models.SchoolModels.Permissions;
using SchoolHubApi.Repositories;
using SchoolHubApi.Results;

namespace SchoolHubApi.Validators;

public class MessageValidator(
    IUserRepository userRepository,
    ICategoryRepository categoryRepository,
    IWorkRepository workRepository,
    ITableRepository tableRepository,
    IMessageRepository messageRepository
)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IWorkRepository _workRepository = workRepository;
    private readonly ITableRepository _tableRepository = tableRepository;
    private readonly IMessageRepository _messageRepository = messageRepository;

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

    public async Task<Result> ValidateAproveMessageAsync(Guid aproverId, Guid messageId, CancellationToken ct = default)
    {
        if (!await _userRepository.UserExistAsync(aproverId, ct))
            return Result.Fail(new NotFoundError("aproverId", "User not found."));

        if (!await _messageRepository.MessageExistAsync(messageId, ct))
            return Result.Fail(new NotFoundError("messageId", "Message not found."));

        var tableId = await _messageRepository.GetMessageTableIdAsync(messageId, ct);

        if (tableId is null)
            return Result.Fail(new NotFoundError("messageId", "Message not found."));

        if (!await _tableRepository.IsMemberAsync(aproverId, tableId.Value, ct))
            return Result.Fail(new ForbiddenError("aproverId", "User is not a member of the message's table."));

        var role = await _tableRepository.GetMemberRoleAsync(aproverId, tableId.Value, ct);

        if (role is null || role is not (TableRole.Owner or TableRole.Admin))
            return Result.Fail(new ForbiddenError("aproverId", "User is not an admin or owner of the message's table."));

        return Result.Ok();
    }
}
