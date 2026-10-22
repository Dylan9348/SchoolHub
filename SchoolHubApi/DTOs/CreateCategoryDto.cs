
namespace SchoolHubApi.DTOs;

public record CreateCategoryDto(
    Guid TableId,
    string CategoryName
);
