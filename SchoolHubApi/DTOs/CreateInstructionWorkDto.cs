
namespace SchoolHubApi.DTOs;

public record CreateInstructionWorkDto(
    Guid CreatorId,
    string Instruction,
    string Title,
    uint MaxValue = 0,
    bool IsHomework = false,
    DateOnly? Date = null
);
