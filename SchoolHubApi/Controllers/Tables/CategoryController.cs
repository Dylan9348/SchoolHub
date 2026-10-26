
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
        if (User.GetUserId() is null)
            return Unauthorized("Invalid Claim");

        // FIX: this never verifies when the user is a member of the table, or if they are an admin or owner

        var result = await _categoryService.CreateCategoryAsync(req);

        return result.ToActionResult();
    }
    
    [HttpDelete("{categoryId:guid}")]
    public async Task<IActionResult> DeleteCategory(Guid categoryId) => StatusCode(StatusCodes.Status501NotImplemented);
}