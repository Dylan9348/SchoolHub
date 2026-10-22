
using FluentResults;
using SchoolHubApi.DTOs;

namespace SchoolHubApi.Services.Tables;

public interface ICategoryService
{
    Task<Result> CreateCategoryAsync(CreateCategoryDto category);
}
