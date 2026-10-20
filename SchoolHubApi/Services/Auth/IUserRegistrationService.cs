
using FluentResults;
using SchoolHubApi.DTOs;
using SchoolHubApi.Enums;
using SchoolHubApi.Models.UserModels;

namespace SchoolHubApi.Services.Auth;

public interface IUserRegistrationService
{
    Task<Result<string>> StartRegisterWithEmailAsync(string username, string password, string email, UserRole role, Language language);
    Task<Result> VerifyAndFinishAsync(string token, string code);
    Task<Result> RegisterStudentUserWithoutEmailAsync(string username, string password);
    Task<Result<string?>> RegisterStudentUser(CreateStudentUserDto student, Language language);
}
