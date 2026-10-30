namespace SchoolHubApi.DTOs;

public record GetMessagesQueryDto(
    Guid CategoryId,
    ushort PageSize,
    ushort Page,
    DateOnly? AfterTo = null,
    DateOnly? BeforeTo = null,
    DateOnly? WorkDate = null,
    string? Title = null,
    string? SendedBy = null,
    bool? IsPending = null
);
