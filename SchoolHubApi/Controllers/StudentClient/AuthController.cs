
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolHubApi.DTOs;
using SchoolHubApi.Models.Exceptions;
using SchoolHubApi.Models.Languages;
using SchoolHubApi.Models.ValidationResultModels;
using SchoolHubApi.Services.Auth;
using SchoolHubApi.Validators;

namespace SchoolHubApi.Controllers.StudentClient;

[Controller]
[Route("student/auth")]
public class AuthStudentController(IUserRegistrationService userRegistrationService, PendingRegistrationValidator pendingValidator) : Controller
{
    private readonly IUserRegistrationService _userRegistrationService = userRegistrationService;
    private readonly PendingRegistrationValidator _pendingValidator = pendingValidator;

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] StudentUserDto req, [FromQuery] string language = SupportedLanguages.English)
    {
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
        }
    }

    [HttpPost("signin/verify-email/{token}")]
    public async Task<IActionResult> VerifyEmail([FromRoute] string token, [FromBody] string code)
    {
        var isValid = await _pendingValidator.IsValid(token);

        if (isValid is FailedValidationResult<string, object?>)
            return BadRequest($"invalid token");

        var finished = await _userRegistrationService.VerifyAndFinishAsync(token, code);

        if (!finished)
            return BadRequest();

        return Created();
    }
}
