
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolHubApi.DTOs;
using SchoolHubApi.Extensions;
using SchoolHubApi.Services.General;

namespace SchoolHubApi.Controllers.General;

[ApiController]
[Authorize]
[Route("[controller]")]
public class WorkController(IWorkService workService) : ControllerBase
{
    private readonly IWorkService _workService = workService;

    [HttpPost("instructions")]
    public async Task<IActionResult> CreateInstructionWork([FromBody] CreateInstructionWorkDto req)
    {
        if (User.GetUserId() is null)
            return Unauthorized("Invalid Claim");

        var result = await _workService.CreateInstructionWorkAsync(req);

        return result.ToActionResult();
    }

    [HttpPost("quiz")]
    public async Task<IActionResult> CreateQuizWork([FromBody] CreateQuizWorkDto req)
    {
        if (User.GetUserId() is null)
            return Unauthorized("Invalid Claim");

        var result = await _workService.CreateQuizWorkAsync(req);

        return result.ToActionResult();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetWork(Guid id)
    {
        var work = await _workService.GetWorkAsync(id);

        return work is null ? NotFound() : Ok(work);
    }
}
