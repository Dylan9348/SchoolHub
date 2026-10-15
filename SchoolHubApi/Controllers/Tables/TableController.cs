
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolHubApi.DTOs;
using SchoolHubApi.Enums;
using SchoolHubApi.Extensions;
using SchoolHubApi.Repositories;
using SchoolHubApi.Validators;
using SchoolHubApi.Validators.ValidationResults;

namespace SchoolHubApi.Controllers.Tables;

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
    public async Task<IActionResult> CreateTable([FromBody] CreateTableDto req, CancellationToken ct)
    {
        var userStringId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userStringId, out var userId))
            return Unauthorized("Invalid Claim");

        var userExists = await _userRepository.UserExistAsync(userId, ct);
        
        if (!userExists)
            return Unauthorized("User no longer exists");
        
        var result = await _tableValidator.ValidateTableCreationAsync(userId, req.TableName);

        if (result is FailedValidationResult failedResult)
        {
            if (failedResult.Type == FailedValidationType.TooShort)
                return BadRequest("Table name is too short. It must be 2 chars of length at least");
            
            if (failedResult.Type == FailedValidationType.TooLong)
                return BadRequest("Table name is too long. It must be 50 chars of length at most");
            
            return Conflict(failedResult.Message);
        }

        var tableId = await _tableRepository.CreateTableAsync(userId, req.TableName);

        return Created(nameof(GetTable), new { id = tableId });
    }

    [HttpGet("{tableId:guid}")]
    public async Task<IActionResult> GetTable([FromRoute] Guid tableId, CancellationToken ct)
    {
        var table = await _tableRepository.GetTableByIdAsync(tableId, ct);

        if (table is null)
            return NotFound();
        
        return Ok(table.ToDto());
    }

    [HttpGet]
    public async Task<IActionResult> SearchTable([FromQuery] SearchTableDto req)
    {
        var table = await _tableRepository.GetTableAsync(req.OwnerName, req.TableName);

        if (table is null)
            return NotFound();
        
        return Ok(table.ToDto());
    }

    [HttpPost("{tableId:guid}/join")]
    public async Task<IActionResult> JoinTable([FromRoute] Guid tableId, CancellationToken ct)
    {
        var userStringId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userStringId, out var userId))
            return Unauthorized("Invalid Claim");

        var validationResult = await _tableValidator.ValidateTableAddMemberAsync(userId, tableId, ct);

        if (validationResult is FailedValidationResult failedResult)
        {
            if (failedResult.Type == FailedValidationType.NotFound)
            {
                if (failedResult.WrongValue is Guid wrongId && wrongId == userId)
                    return Unauthorized("User no longer exists");

                return NotFound(failedResult.Message);
            }

            if (failedResult.Type == FailedValidationType.Conflict)
                return Conflict(failedResult.Message);

            return BadRequest(failedResult.Message);
        }

        var added = await _tableRepository.AddMemberAsync(userId, tableId, ct);

        if (!added)
            return Conflict("Already member");

        return Ok();
    }
}
