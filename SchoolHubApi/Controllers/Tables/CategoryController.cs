
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolHubApi.DTOs;
using SchoolHubApi.Extensions;
using SchoolHubApi.Services.Tables;

namespace SchoolHubApi.Controllers.Tables;

[ApiController]
[Authorize]
[Route("[controller]")]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto req)
    {
        var userId = User.GetUserId();

        if (userId is null)
            return Unauthorized("Invalid Claim");

        var result = await _categoryService.CreateCategoryAsync(userId.Value, req);

        return result.ToActionResult();
    }
}