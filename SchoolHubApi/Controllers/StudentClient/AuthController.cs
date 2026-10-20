
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using SchoolHubApi.DTOs;
using SchoolHubApi.Enums;
using SchoolHubApi.Extensions;
using SchoolHubApi.Repositories;
using SchoolHubApi.Results;
using SchoolHubApi.Services.Auth;
using SchoolHubApi.Validators;

namespace SchoolHubApi.Controllers.StudentClient;

[ApiController]
[Route("student/auth")]
public class AuthStudentController(
    IUserRegistrationService userRegistrationService, 
    PendingRegistrationValidator pendingValidator,
    IAuthService authService,
    IUserRepository userRepository
) : Controller
{
    private readonly IUserRegistrationService _userRegistrationService = userRegistrationService;
    private readonly PendingRegistrationValidator _pendingValidator = pendingValidator;
    private readonly IAuthService _authService = authService;
    private readonly IUserRepository _userRepository = userRepository;

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] CreateStudentUserDto req, [FromQuery] Language language = Language.English)
    {
        var result = await _userRegistrationService.RegisterStudentUser(req, language);

        if (result.IsFailed)
            return result.ToActionResult();

        var token = result.Value;

        if (token is null)
            return Created();

        return Ok(token);
    }

    [HttpPost("signin/verify-email/{code}")]
    public async Task<IActionResult> VerifyEmail([FromBody] string token, [FromRoute] string code)
    {
        var validation = await _pendingValidator.IsValid(token);

        if (validation.IsFailed)
            return validation.ToActionResult();

        var result = await _userRegistrationService.VerifyAndFinishAsync(token, code);

        if (result.IsFailed)
            return result.ToActionResult();

        return Created();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] LogInUserDto req)
    {
        var user = await _userRepository.GetUserByUsernameAsync(req.Username);

        if (user is null)
            return Result.Fail(new NotFoundError("username", $"user '{req.Username}' does not exists")).ToActionResult();

        var result = _authService.AuthenticateUser(user, req.Password);

        if (result.IsFailed)
            return result.ToActionResult();

        return Ok(result.Value);
    }
}
