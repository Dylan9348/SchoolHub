
using SchoolHubApi.DTOs;
using SchoolHubApi.Enums;
using SchoolHubApi.Models.UserModels;

namespace SchoolHubApi.Services.Auth;

public interface IUserRegistrationService
{
    Task<string> StartRegisterWithEmailAsync(string username, string password, string email, UserRole role, Language language);
    Task<bool> VerifyAndFinishAsync(string token, string code);
    Task RegisterStudentUserWithoutEmailAsync(string username, string password);
    Task<string?> RegisterStudentUser(CreateStudentUserDto student, Language language);
}
