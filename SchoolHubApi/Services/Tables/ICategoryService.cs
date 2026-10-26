
using FluentResults;
using SchoolHubApi.DTOs;

namespace SchoolHubApi.Services.Tables;

public interface ICategoryService
{
    Task<Result> CreateCategoryAsync(Guid userId, CreateCategoryDto category);
}
