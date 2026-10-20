
using FluentResults;
using SchoolHubApi.DTOs;
using SchoolHubApi.Enums;
using SchoolHubApi.Models.UserModels;
using SchoolHubApi.Repositories;
using SchoolHubApi.Validators;

namespace SchoolHubApi.Services.Auth;

public class UserRegistrationService(
    IUserRepository userRepository,
    ITokenAuthService tokenService, 
    IEmailAuthService emailAuthService,
    PendingRegistrationValidator pendingRegistrationValidator) : IUserRegistrationService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITokenAuthService _tokenService = tokenService;
    private readonly IEmailAuthService _emailService = emailAuthService;
    private readonly PendingRegistrationValidator _pendingRegistrationValidator = pendingRegistrationValidator;

    public async Task<Result<string>> StartRegisterWithEmailAsync(string username, string password, string email, UserRole role, Language language)
    {
        var code = _tokenService.GenerateSecureRandomString(6);
        var token = _tokenService.GenerateSecureRandomString(10);

        var saveResult = await _userRepository.SavePendingUserAsync(username, email, password, role, code, token);

        if (saveResult.IsFailed)
            return Result.Fail<string>(saveResult.Errors);

        var mailResult = await _emailService.SendVerificationMailAsync(email, language, code);

        if (mailResult.IsFailed)
            return Result.Fail<string>(mailResult.Errors);

        return Result.Ok(token);
    }

    public async Task<Result> VerifyAndFinishAsync(string token, string code)
    {
        var validationResult = await _pendingRegistrationValidator.ValidateCodeAsync(token, code);

        if (validationResult.IsFailed)
            return validationResult;

        var acceptResult = await _userRepository.AcceptPendingUserAsync(token);

        if (acceptResult.IsFailed)
            return acceptResult;

        return Result.Ok();
    }

    public async Task<Result> RegisterStudentUserWithoutEmailAsync(string username, string password)
    {
        return await _userRepository.SaveStudentUserWithoutEmailAsync(username, password);
    }

    public async Task<Result<string?>> RegisterStudentUser(CreateStudentUserDto student, Language language)
    {
        if (student.Email is null)
        {
            var result = await RegisterStudentUserWithoutEmailAsync(student.Username, student.Password);

            if (result.IsFailed)
                return Result.Fail<string?>(result.Errors);

            return Result.Ok<string?>(null);
        }

        var emailResult = await StartRegisterWithEmailAsync(student.Username, student.Password, student.Email, UserRole.Student, language);

        if (emailResult.IsFailed)
            return Result.Fail<string?>(emailResult.Errors);

        return Result.Ok<string?>(emailResult.Value);
    }
}
