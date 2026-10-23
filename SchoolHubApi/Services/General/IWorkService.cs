
using FluentResults;
using SchoolHubApi.DTOs;
using SchoolHubApi.Models.WorkModels;

namespace SchoolHubApi.Services.General;

public interface IWorkService
{
    Task<Result> CreateInstructionWorkAsync(CreateInstructionWorkDto work);
    Task<Result> CreateQuizWorkAsync(CreateQuizWorkDto work);
    Task<Work?> GetWorkAsync(Guid id);
}
