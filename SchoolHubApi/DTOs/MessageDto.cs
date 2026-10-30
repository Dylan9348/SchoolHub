namespace SchoolHubApi.DTOs;

public record MessageDto(
    Guid Id,
    DateOnly Date,
    Guid CategoryId,
    string Sender,
    bool Pending,
    WorkDto Work
);
