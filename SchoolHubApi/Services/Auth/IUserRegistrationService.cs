
using SchoolHubApi.DTOs;
using SchoolHubApi.Models.UserModels;

namespace SchoolHubApi.Services.Auth;

public interface IUserRegistrationService
{
    Task<string> StartRegisterWithEmailAsync(string username, string password, string email, UserRole role, string language);
    Task<bool> VerifyAndFinishAsync(string token, string code);
    Task RegisterStudentUserWithoutEmailAsync(string username, string password);
    Task<string?> RegisterStudentUser(CreateStudentUserDto student, string language);
}
