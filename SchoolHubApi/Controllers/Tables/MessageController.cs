
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolHubApi.DTOs;
using SchoolHubApi.Extensions;
using SchoolHubApi.Services.Tables;

namespace SchoolHubApi.Controllers.Tables;


[ApiController]
[Authorize]
[Route("[controller]")]
public class MessageController(
    IMessageService messageService
) : ControllerBase
{
    private readonly IMessageService _messageService = messageService;

    [HttpPost("send/{categoryId:guid}")]
    public async Task<IActionResult> SendMessage(Guid categoryId, [FromBody] SendMessageDto req, CancellationToken ct) 
    {
        var userId = User.GetUserId();

        if (userId is null)
            return Unauthorized();
        
        var result = await _messageService.SendMessageAsync((Guid)userId, req.WorkId, categoryId, ct);

        return result.ToActionResult();
    }

    [HttpPost("aprove/{messageId:guid}")]
    public async Task<IActionResult> AproveMessage(Guid messageId)
    {
        var userId = User.GetUserId();

        if (userId is null)
            return Unauthorized();

        var result = await _messageService.AprovePendingMessageAsync((Guid)userId, messageId);

        return result.ToActionResult();
    }
}