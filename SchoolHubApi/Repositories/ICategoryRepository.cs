
using SchoolHubApi.Models.TableModels;

namespace SchoolHubApi.Repositories;

public interface ICategoryRepository
{
    Task<Category?> GetCategoryAsync(Guid categoryId, CancellationToken ct = default);
    Task<Guid> CreateCategoryAsync(Guid tableId, string categoryName);
    Task<bool> CategoryExist(Guid tableId, string categoryName);
}
