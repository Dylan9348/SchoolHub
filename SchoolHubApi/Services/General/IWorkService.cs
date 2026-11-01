
using FluentResults;
using SchoolHubApi.DTOs;
using SchoolHubApi.Models.WorkModels;

namespace SchoolHubApi.Services.General;

public interface IWorkService
{
    Task<Result> CreateInstructionWorkAsync(Guid creatorId, CreateInstructionWorkDto work);
    Task<Result> CreateQuizWorkAsync(Guid creatorId, CreateQuizWorkDto work);
    Task<Work?> GetWorkAsync(Guid id);
}
