
namespace SchoolHubApi.DTOs;

public record CreateInstructionWorkDto(
    string Instruction,
    string Title,
    uint MaxValue = 0,
    bool IsHomework = false,
    DateOnly? Date = null
);
