
using System.Security.Cryptography;
using System.Text;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using SchoolHubApi.Data;
using SchoolHubApi.Models.AuthModels;
using SchoolHubApi.Results;
using SchoolHubApi.Repositories;
using SchoolHubApi.Enums;

namespace SchoolHubApi.Validators;

public class PendingRegistrationValidator(Context database, IUserRepository userRepository)
{
    private readonly Context _database = database;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result> IsValid(string token)
    {
        var pending = await _database.PendingRegistrations.FirstOrDefaultAsync(r => r.Token == token);

        if (pending is null)
            return Result.Fail(new NotFoundError("token", "registration not found"));

        return await IsValid(pending);
    }

    public async Task<Result> IsValid(PendingRegistration pending)
    {
        if (pending.ExpiresAt < DateTime.UtcNow)
            return Result.Fail(new ExpiredError("expired"));

        if (!pending.Valid)
            return Result.Fail(new InvalidError("pendingRegistration", "invalid"));

        if (pending.Attempts <= 0)
            return Result.Fail(new NoAttemptsLeftError("no attempts left"));

        var usernameAlreadyTaken = await _database.Users.AnyAsync(u => u.Username == pending.Username);
        if (usernameAlreadyTaken)
            return Result.Fail(new AlreadyTakenError("username", "username already taken"));

        var emailAlreadyTaken = await _database.Users.AnyAsync(u => u.Email == pending.Email);
        if (emailAlreadyTaken)
            return Result.Fail(new AlreadyTakenError("email", "email already taken"));

        return Result.Ok();
    }

    public async Task<Result> ValidateCodeAsync(string token, string code)
    {
        var pending = await _userRepository.GetPendingRegistrationAsync(token);

        if (pending is null)
            return Result.Fail(new NotFoundError("token", "pending registration does not exist"));
        
        var validation = await IsValid(pending);

        if (validation.IsFailed)
            return validation;
        
        var secret = Environment.GetEnvironmentVariable("CODE_HASH_SECRET")
            ?? throw new InvalidOperationException("MISSING ENVIRONMENT VARIABLE: CODE_HASH_SECRET");

        var secretBytes = Convert.FromBase64String(secret);

        byte[] submittedHash = HMACSHA256.HashData(secretBytes, Encoding.UTF8.GetBytes(code));
        
        var isValid = CryptographicOperations.FixedTimeEquals(submittedHash, pending.CodeHash);

        if (isValid)
            return Result.Ok();

        return Result.Fail(new InvalidError("code", "invalid code"));
    }
}
