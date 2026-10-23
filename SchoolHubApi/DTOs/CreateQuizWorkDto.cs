
namespace SchoolHubApi.DTOs;

public record CreateQuizWorkDto(
    Guid CreatorId,
    Dictionary<string, string?> Questions,
    string Title,
    uint MaxValue = 0,
    bool IsHomework = false,
    DateOnly? Date = null
);
