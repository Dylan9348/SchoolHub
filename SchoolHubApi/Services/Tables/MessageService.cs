
using FluentResults;
using SchoolHubApi.Models.SchoolModels.Permissions;
using SchoolHubApi.Repositories;
using SchoolHubApi.Validators;

namespace SchoolHubApi.Services.Tables;

public class MessageService(
    ITableRepository tableRepository,
    ICategoryRepository categoryRepository,
    IMessageRepository messageRepository,
    MessageValidator messageValidator
) : IMessageService
{
    private readonly ITableRepository _tableRepository = tableRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IMessageRepository _messageRepository = messageRepository;
    private readonly MessageValidator _messageValidator = messageValidator;

    public async Task<Result> SendMessageAsync(Guid userId, Guid workId, Guid categoryId, CancellationToken ct = default)
    {
        var validationResult = await _messageValidator.ValidateSendMessageAsync(userId, categoryId, workId, ct);

        if (validationResult.IsFailed)
            return validationResult;

        var category = await _categoryRepository.GetCategoryAsync(categoryId, ct);

        var role = await _tableRepository.GetMemberRoleAsync(userId, category!.TableId, ct);

        var pending = role is not (TableRole.Owner or TableRole.Admin);

        await _messageRepository.CreateMessageAsync(userId, categoryId, workId, pending, ct);

        return Result.Ok();
    }

    public async Task<Result> AprovePendingMessageAsync(Guid aproverId, Guid messageId, CancellationToken ct = default)
    {
        var validationResult = await _messageValidator.ValidateAproveMessageAsync(aproverId, messageId, ct);

        if (validationResult.IsFailed)
            return validationResult;
            
        var result = await _messageRepository.AproveMessageAsync(messageId);

        if (result.IsFailed)
            return result;

        return Result.Ok();
    }
}
