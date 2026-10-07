
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SchoolHubApi.Data;
using SchoolHubApi.Models.AuthModels;
using SchoolHubApi.Models.Exceptions;
using SchoolHubApi.Models.UserModels;

namespace SchoolHubApi.Repositories;

public class UserRepository(Context database) : IUserRepository
{
    private readonly Context _database = database;

    public async Task<StudentUser?> GetStudentUserByIdAsync(Guid id)
    {
        var user = await _database.Users.FindAsync(id);

        if (user is not StudentUser studentUser)
            return null;
        
        return studentUser;
    }

    public async Task<PendingRegistration?> GetPendingRegistrationAsync(Guid id)
    {
        var user = await _database.PendingRegistrations.FindAsync(id);

        return user;
    }

    public async Task<PendingRegistration?> GetPendingRegistrationAsync(string token)
    {
        var pending = await _database.PendingRegistrations.FirstOrDefaultAsync(r => r.Token == token);

        return pending;
    }

    public async Task SaveStudentUserWithoutEmailAsync(string username, string password)
    {
        var existsOther = await _database.Users.AnyAsync(u => u.Username == username);

        if (existsOther)
            throw new UsernameAlreadyTakenException(username);

        var user = new StudentUser()
        {
            Username = username,
            Password = BCrypt.Net.BCrypt.HashPassword(password)
        };

        _database.Add(user);
        await _database.SaveChangesAsync();
    }

    public async Task SavePendingUserAsync(string username, string email, string password, string role, string code, string token)
    {
        var secret = Environment.GetEnvironmentVariable("CODE_HASH_SECRET")
            ?? throw new InvalidOperationException("MISSING ENVIRONMENT VARIABLE: CODE_HASH_SECRET");

        var secretBytes = Convert.FromBase64String(secret);

        var existsOtherWithSameToken = await _database.PendingRegistrations.AnyAsync(r => r.Token == token);

        if (existsOtherWithSameToken)
            throw new InvalidOperationException("Exists other pending registration with the same token.");

        var usernameAlreadyTaken = await _database.Users.AnyAsync(u => u.Username == username);

        if (usernameAlreadyTaken)
            throw new UsernameAlreadyTakenException(username);

        var pending = new PendingRegistration
        {
            Token = token,
            Username = username,
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            CodeHash = HMACSHA256.HashData(secretBytes, Encoding.UTF8.GetBytes(code)),
            Role = role
        };

        _database.Add(pending);
        await _database.SaveChangesAsync();
    }
    public async Task<bool> AcceptPendingUserAsync(string token)
    {
        var pending = await _database.PendingRegistrations.FirstOrDefaultAsync(p => p.Token == token)
            ?? throw new NotFoundException("Pending register not found");
        
        SchoolHubUser user = pending.Role switch
        {
            UserRole.Student => new StudentUser()
            {
                Username = pending.Username,
                Password = pending.PasswordHash,
                Email = pending.Email,

            },
            UserRole.Teacher => new TeacherUser()
            {
                Username = pending.Username,
                Password = pending.PasswordHash,
                Email = pending.Email
            },
            _ => throw new InvalidOperationException($"Invalid user role ({pending.Role}). "),
        };
        
        _database.Add(user);
        await _database.SaveChangesAsync();

        return true;
    }
}
