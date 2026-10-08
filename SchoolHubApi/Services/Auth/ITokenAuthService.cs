
using SchoolHubApi.Models.UserModels;

namespace SchoolHubApi.Services.Auth;


public interface ITokenAuthService
{
    string GenerateCodeEmailVerification();
    string GenerateSecureRandomString(int length = 10);
    string GenerateJwt(Guid userId, UserRole role, string email);
}