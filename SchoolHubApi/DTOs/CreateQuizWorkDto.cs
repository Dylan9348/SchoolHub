
namespace SchoolHubApi.DTOs;

public record CreateQuizWorkDto(
    Dictionary<string, string?> Questions,
    string Title,
    uint MaxValue = 0,
    bool IsHomework = false,
    DateOnly? Date = null
);
