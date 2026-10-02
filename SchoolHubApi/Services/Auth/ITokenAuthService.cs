
namespace SchoolHubApi.Services.Auth;


public interface ITokenAuthService
{
    string GenerateCodeEmailVerification();
    string GenerateSecureRandomString(int length = 10);
}