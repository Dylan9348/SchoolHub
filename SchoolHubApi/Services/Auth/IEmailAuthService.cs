
namespace SchoolHubApi.Services.Auth;

public interface IEmailAuthService
{
    Task<bool> SendVerificationMailAsync(string email, string language, string code);
}
