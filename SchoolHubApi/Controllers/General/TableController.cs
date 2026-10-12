
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolHubApi.DTOs;
using SchoolHubApi.Repositories;
using SchoolHubApi.Validators;
using SchoolHubApi.Validators.ValidationResult;

namespace SchoolHubApi.Controllers.General;

[ApiController]
[Route("[controller]")]
[Authorize]
public class TableController(
    ITableRepository tableRepository,
    IUserRepository userRepository, 
    TableValidator tableValidator
) : ControllerBase
{
    private readonly ITableRepository _tableRepository = tableRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly TableValidator _tableValidator = tableValidator;

    [HttpPost("create")]
    public async Task<IActionResult> CreateTable(CreateTableDto req)
    {
        var userStringId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userStringId, out var userId))
            return Unauthorized("Invalid Claim");

        var userExists = await _userRepository.UserExistAsync(userId);
        
        if (!userExists)
            return Unauthorized("User not found");
        
        var result = await _tableValidator.ValidateTableCreationAsync(userId, req.TableName);

        if (result is FailedValidationResult<object?, string> failedResult)
        {
            if (failedResult.Message == "too short")
                return BadRequest("Table name is too short. It must be 2 chars of length at least");
            
            if (failedResult.Message == "too long")
                return BadRequest("Table name is too long. It must be 50 chars of length at most");
            
            return Conflict(failedResult.Message);
        }

        var tableId = await _tableRepository.CreateTableAsync(userId, req.TableName);

        return Created($"/table/{tableId}", new { id = tableId });
    }
}
