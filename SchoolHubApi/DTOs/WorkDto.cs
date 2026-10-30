namespace SchoolHubApi.DTOs;

public record WorkDto(
    Guid Id,
    string Title,
    DateOnly Date
);
