
using SchoolHubApi.Models.UserModels;

namespace SchoolHubApi.Services.Auth;

public class AuthService(ITokenAuthService tokenService) : IAuthService
{
    private readonly ITokenAuthService _tokenService = tokenService;

    public string? AuthenticateUser(SchoolHubUser user, string password)
    {
        var passwordIsCorrect = BCrypt.Net.BCrypt.Verify(password, user.Password);

        if (!passwordIsCorrect)
            return null;

        UserRole? role = user switch
        {
            StudentUser => UserRole.Student,
            TeacherUser => UserRole.Teacher,
            _ => null
        };

        if (role is null)
            return null;
        
        return _tokenService.GenerateJwt(user.Id, (UserRole)role, user.Email);
    }
}
