
using FluentResults;
using SchoolHubApi.Models.UserModels;
using SchoolHubApi.Results;

namespace SchoolHubApi.Services.Auth;

public class AuthService(ITokenAuthService tokenService) : IAuthService
{
    private readonly ITokenAuthService _tokenService = tokenService;

    public Result<string> AuthenticateUser(SchoolHubUser user, string password)
    {
        var passwordIsCorrect = BCrypt.Net.BCrypt.Verify(password, user.Password);

        if (!passwordIsCorrect)
            return Result.Fail<string>(new InvalidError("password", "wrong password"));

        UserRole? role = user switch
        {
            StudentUser => UserRole.Student,
            TeacherUser => UserRole.Teacher,
            _ => null
        };

        if (role is null)
            return Result.Fail<string>(new InvalidError("role", "unsupported user role"));
        
        return Result.Ok(_tokenService.GenerateJwt(user.Id, (UserRole)role, user.Email));
    }
}
