
using FluentResults;
using SchoolHubApi.Models.AuthModels;
using SchoolHubApi.Models.UserModels;

namespace SchoolHubApi.Repositories;

public interface IUserRepository
{
    Task<StudentUser?> GetStudentUserByIdAsync(Guid id);
    Task<SchoolHubUser?> GetUserByIdAsync(Guid id);
    Task<bool> UserExistAsync(Guid id, CancellationToken ct = default);
    Task<SchoolHubUser?> GetUserByUsernameAsync(string username);
    Task<PendingRegistration?> GetPendingRegistrationAsync(Guid id);
    Task<PendingRegistration?> GetPendingRegistrationAsync(string token);
    Task<Result> SavePendingUserAsync(string username, string email, string password, UserRole role, string code, string token);
    Task<Result> AcceptPendingUserAsync(string pendingId);
    Task<Result> SaveStudentUserWithoutEmailAsync(string username, string password);
}
