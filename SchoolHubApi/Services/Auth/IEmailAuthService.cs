
using FluentResults;
using SchoolHubApi.Enums;

namespace SchoolHubApi.Services.Auth;

public interface IEmailAuthService
{
    Task<Result> SendVerificationMailAsync(string email, Language language, string code);
}
