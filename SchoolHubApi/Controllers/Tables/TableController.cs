
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolHubApi.DTOs;
using SchoolHubApi.Enums;
using SchoolHubApi.Extensions;
using SchoolHubApi.Repositories;
using SchoolHubApi.Results;
using SchoolHubApi.Validators;

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
        if (User.GetUserId() is not Guid userId)
            return Unauthorized("Invalid Claim");

        var userExists = await _userRepository.UserExistAsync(userId, ct);
        
        if (!userExists)
            return Unauthorized("User no longer exists");
        
        var validation = await _tableValidator.ValidateTableCreationAsync(userId, req.TableName);

        if (validation.IsFailed)
            return validation.ToActionResult();

        var tableId = await _tableRepository.CreateTableAsync(userId, req.TableName);

        return Created(nameof(GetTable), new { id = tableId });
    }

    [HttpGet("{tableId:guid}")]
    public async Task<IActionResult> GetTable([FromRoute] Guid tableId, CancellationToken ct)
    {
        var table = await _tableRepository.GetTableByIdAsync(tableId, ct);

        if (table is null)
            return Result.Fail(new NotFoundError("tableId", "Table not found.")).ToActionResult();
        
        return Ok(table.ToDto());
    }

    [HttpGet]
    public async Task<IActionResult> SearchTable([FromQuery] SearchTableDto req)
    {
        var table = await _tableRepository.GetTableAsync(req.OwnerName, req.TableName);

        if (table is null)
            return Result.Fail(new NotFoundError("tableName", "Table not found.")).ToActionResult();
        
        return Ok(table.ToDto());
    }

    [HttpPost("{tableId:guid}/join")]
    public async Task<IActionResult> JoinTable([FromRoute] Guid tableId, CancellationToken ct)
    {
        if (User.GetUserId() is not Guid userId)
            return Unauthorized("Invalid Claim");

        var validation = await _tableValidator.ValidateTableAddMemberAsync(userId, tableId, ct);

        if (validation.IsFailed)
        {
            if (validation.Errors.FirstOrDefault() is NotFoundError { Field: "userId" })
                return Unauthorized("User no longer exists");

            return validation.ToActionResult();
        }

        var added = await _tableRepository.AddMemberAsync(userId, tableId, ct);

        if (!added)
            return Result.Fail(new ConflictError("tableId", "Already member")).ToActionResult();

        return Ok();
    }
}
