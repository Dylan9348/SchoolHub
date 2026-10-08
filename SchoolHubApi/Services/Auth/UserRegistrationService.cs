
using SchoolHubApi.DTOs;
using SchoolHubApi.Models.AuthModels;
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

    public async Task<string> StartRegisterWithEmailAsync(string username, string password, string email, UserRole role, string language)
    {
        var code = _tokenService.GenerateSecureRandomString(6);
        var token = _tokenService.GenerateSecureRandomString(10);

        await _userRepository.SavePendingUserAsync(username, email, password, role, code, token);
        await _emailService.SendVerificationMailAsync(email, language, code);

        return token;
    }

    public async Task<bool> VerifyAndFinishAsync(string token, string code)
    {
        var isValid = await _pendingRegistrationValidator.ValidateCodeAsync(token, code);

        var accepted = await _userRepository.AcceptPendingUserAsync(token);

        if (!isValid | !accepted)
            return false;

        return true;
    }

    public async Task RegisterStudentUserWithoutEmailAsync(string username, string password)
    {
        await _userRepository.SaveStudentUserWithoutEmailAsync(username, password);
    }

    public async Task<string?> RegisterStudentUser(CreateStudentUserDto student, string language)
    {
        if (student.Email is null)
            await RegisterStudentUserWithoutEmailAsync(student.Username, student.Password);
        else
            return await StartRegisterWithEmailAsync(student.Username, student.Password, student.Email, UserRole.Student, language);
        
        return null;
    }
}
