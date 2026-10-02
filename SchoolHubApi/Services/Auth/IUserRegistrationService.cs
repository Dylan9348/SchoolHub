
using SchoolHubApi.DTOs;

namespace SchoolHubApi.Services.Auth;

public interface IUserRegistrationService
{
    Task<string> StartRegisterWithEmailAsync(string username, string password, string email, string role, string language);
    Task<bool> VerifyAndFinishAsync(string token, string code);
    Task RegisterStudentUserWithoutEmailAsync(string username, string password);
    Task<string?> RegisterStudentUser(StudentUserDto student, string language);
}
