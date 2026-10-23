
using FluentResults;
using SchoolHubApi.Repositories;
using SchoolHubApi.Results;

namespace SchoolHubApi.Validators;

public class WorkValidator(IUserRepository userRepository)
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result> ValidateWorkCreation(Guid userId)
    {
        var userExists = await _userRepository.UserExistAsync(userId);

        if (!userExists)
            return Result.Fail(new NotFoundError("user", $"A user with the id '{userId}' does not exist"));

        return Result.Ok();
    }
}
