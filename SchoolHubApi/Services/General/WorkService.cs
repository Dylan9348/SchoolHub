
using FluentResults;
using SchoolHubApi.DTOs;
using SchoolHubApi.Models.WorkModels;
using SchoolHubApi.Repositories;
using SchoolHubApi.Validators;

namespace SchoolHubApi.Services.General;

public class WorkService(
    IWorkRepository workRepository,
    WorkValidator workValidator) : IWorkService
{
    private readonly IWorkRepository _workRepository = workRepository;
    private readonly WorkValidator _workValidator = workValidator;

    public async Task<Result> CreateInstructionWorkAsync(CreateInstructionWorkDto work)
    {
        var validationResult = await _workValidator.ValidateWorkCreation(work.CreatorId);

        if (validationResult.IsFailed)
            return validationResult;

        await _workRepository.CreateInstructionWorkAsync(work.CreatorId, work.Instruction, work.Title, work.MaxValue, work.IsHomework, work.Date);

        return Result.Ok();
    }

    public async Task<Result> CreateQuizWorkAsync(CreateQuizWorkDto work)
    {
        var validationResult = await _workValidator.ValidateWorkCreation(work.CreatorId);

        if (validationResult.IsFailed)
            return validationResult;

        await _workRepository.CreateQuizWorkAsync(work.CreatorId, work.Questions, work.Title, work.MaxValue, work.IsHomework, work.Date);

        return Result.Ok();
    }

    public async Task<Work?> GetWorkAsync(Guid id) =>
        await _workRepository.GetWorkAsync(id);
}
