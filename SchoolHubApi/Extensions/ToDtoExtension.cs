
using SchoolHubApi.DTOs;
using SchoolHubApi.Models.TableModels;

namespace SchoolHubApi.Extensions;

public static class ToDtoExtension
{
    public static TableDto ToDto(this Table table)
    {
        var dto = new TableDto(table.Id, table.Name, [.. table.Categories.Select(c => c.CategoryName)]);

        return dto;
    }
}
