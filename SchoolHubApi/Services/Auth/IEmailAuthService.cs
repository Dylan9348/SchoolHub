
using FluentResults;
using SchoolHubApi.Enums;

namespace SchoolHubApi.Services.Auth;

public interface EmailAuthServiceContract
{
    Task<Result> SendVerificationMailAsync(string email, Language language, string code);
}
