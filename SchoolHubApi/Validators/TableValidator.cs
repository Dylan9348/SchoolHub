
using FluentResults;
using SchoolHubApi.Models.SchoolModels.Permissions;
using SchoolHubApi.Repositories;
using SchoolHubApi.Results;

namespace SchoolHubApi.Validators;

public class TableValidator(
    ITableRepository tableRepository,
    IUserRepository userRepository,
    IMemberRepository memberRepository
)
{
    private readonly ITableRepository _tableRepository = tableRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMemberRepository _memberRepository = memberRepository;

    private const int MinTableNameLength = 2;
    private const int MaxTableNameLength = 50;

    public async Task<Result> ValidateTableCreationAsync(Guid userId, string tableName)
    {
        if (tableName.Length < MinTableNameLength)
            return Result.Fail(new TooShortError("tableName", MinTableNameLength, $"Table name is too short. It must be {MinTableNameLength} chars of length at least"));

        if (tableName.Length > MaxTableNameLength)
            return Result.Fail(new TooLongError("tableName", MaxTableNameLength, $"Table name is too long. It must be {MaxTableNameLength} chars of length at most"));

        if (await _tableRepository.OwnerHasTableNamedAsync(userId, tableName))
            return Result.Fail(new ConflictError("tableName", "You already have a table with that name."));

        return Result.Ok();
    }

    public async Task<Result> ValidateTableAddMemberAsync(Guid userId, Guid tableId, CancellationToken ct = default)
    {
        if (!await _userRepository.UserExistAsync(userId, ct))
            return Result.Fail(new NotFoundError("userId", "User not found."));

        if (await _tableRepository.GetTableByIdAsync(tableId, ct) is null)
            return Result.Fail(new NotFoundError("tableId", "Table not found."));

        if (await _tableRepository.IsMemberAsync(userId, tableId, ct))
            return Result.Fail(new ConflictError("userId", "User is already a member of the table."));

        return Result.Ok();
    }

    public async Task<Result> ValidateRoleAssignmentAsync(Guid memberId, CancellationToken ct = default)
    {
        var memberRole = await _memberRepository.GetRoleByIdAsync(memberId, ct);

        if (memberRole is null)
            return Result.Fail(new NotFoundError("memberId", "Member not found."));

        if (memberRole is TableRole.Admin or TableRole.Owner)
            return Result.Fail(new ConflictError("memberId", "Member is already an admin of the table."));

        return Result.Ok();
    }
}