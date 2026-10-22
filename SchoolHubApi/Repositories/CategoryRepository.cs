
using Microsoft.EntityFrameworkCore;
using SchoolHubApi.Data;
using SchoolHubApi.Models.TableModels;

namespace SchoolHubApi.Repositories;

public class CategoryRepository(Context database) : ICategoryRepository
{
    private readonly Context _database = database;

    public async Task<Category?> GetCategoryAsync(Guid categoryId) => 
        await _database.TableCategories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == categoryId);
    
    public async Task<Guid> CreateCategoryAsync(Guid tableId, string categoryName)
    {
        var category = new Category()
        {
            CategoryName = categoryName,
            TableId = tableId
        };

        _database.Add(category);

        await _database.SaveChangesAsync();

        return category.Id;
    }

    public async Task<bool> CategoryExist(Guid tableId, string categoryName) => 
        await _database.TableCategories.AnyAsync(c => c.TableId == tableId && c.CategoryName == categoryName);
}
