
using SchoolHubApi.Models.UserModels;

namespace SchoolHubApi.Services.Auth;

public interface IAuthService
{
    string? AuthenticateUser(SchoolHubUser user, string password);
}
