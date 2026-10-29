
using SchoolHubApi.Models.TableModels;

namespace SchoolHubApi.Repositories;

public interface ICategoryRepository
{
    Task<Category?> GetCategoryAsync(Guid categoryId, CancellationToken ct = default);
    Task<Guid> CreateCategoryAsync(Guid tableId, string categoryName);
    Task<bool> CategoryExistAsync(Guid tableId, string categoryName);
}
