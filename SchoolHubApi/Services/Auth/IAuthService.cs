
using FluentResults;
using SchoolHubApi.Models.UserModels;

namespace SchoolHubApi.Services.Auth;

public interface IAuthService
{
    Result<string> AuthenticateUser(SchoolHubUser user, string password);
}
