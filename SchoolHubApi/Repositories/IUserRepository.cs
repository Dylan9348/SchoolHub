using SchoolHubApi.DTOs;
using SchoolHubApi.Models.AuthModels;
using SchoolHubApi.Models.UserModels;

namespace SchoolHubApi.Repositories;

public interface IUserRepository
{
    Task<StudentUser?> GetStudentUserByIdAsync(Guid id);
    Task<PendingRegistration?> GetPendingRegistrationAsync(Guid id);
    Task<PendingRegistration?> GetPendingRegistrationAsync(string token);
    Task SavePendingUserAsync(string username, string email, string password, string role, string code, string token);
    Task<bool> AcceptPendingUserAsync(string pendingId);
    Task SaveStudentUserWithoutEmailAsync(string username, string password);
}
