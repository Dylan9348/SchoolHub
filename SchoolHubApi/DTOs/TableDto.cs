
namespace SchoolHubApi.DTOs;

public record TableDto(
    Guid Id, 
    string Name, 
    string[] Categories
);
