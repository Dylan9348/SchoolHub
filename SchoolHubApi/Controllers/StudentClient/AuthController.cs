
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolHubApi.DTOs;
using SchoolHubApi.Models.Exceptions;
using SchoolHubApi.Models.Languages;
using SchoolHubApi.Models.ValidationResultModels;
using SchoolHubApi.Repositories;
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
    public async Task<IActionResult> SignIn([FromBody] CreateStudentUserDto req, [FromQuery] string language = SupportedLanguages.English)
    {
        try{
        try
        {
            var token = await _userRegistrationService.RegisterStudentUser(req, language);
            if (token is null)
                return Created();

            return Ok(token);
        }
        catch (LanguageNotSupportedException)
        {
            return BadRequest($"invalid language {language}");
        }}
        catch(Exception e)
        {
            return UnprocessableEntity(e.Message);
        }
    }

    [HttpPost("signin/verify-email/{code}")]
    public async Task<IActionResult> VerifyEmail([FromBody] string token, [FromRoute] string code)
    {
        var isValid = await _pendingValidator.IsValid(token);

        if (isValid is FailedValidationResult<string, object?>)
            return BadRequest($"invalid token");

        var finished = await _userRegistrationService.VerifyAndFinishAsync(token, code);

        if (!finished)
            return BadRequest();

        return Created();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] LogInUserDto req)
    {
        var user = await _userRepository.GetUserByUsernameAsync(req.Username);

        if (user is null)
            return BadRequest($"user '{req.Username}' does not exists");

        var token = _authService.AuthenticateUser(user, req.Password);

        if (token is null)
            return BadRequest("wrong password");

        return Ok(token);
    }
}
