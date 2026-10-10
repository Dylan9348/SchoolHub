
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SchoolHubApi.Data;
using SchoolHubApi.Models.AuthModels;
using SchoolHubApi.Exceptions;
using SchoolHubApi.Validators.ValidationResult;
using SchoolHubApi.Repositories;

namespace SchoolHubApi.Validators;

public class PendingRegistrationValidator(Context database, IUserRepository userRepository)
{
    private readonly Context _database = database;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ValidationResult<string>> IsValid(string token)
    {
        var pending = await _database.PendingRegistrations.FirstOrDefaultAsync(r => r.Token == token);

        if (pending is null)
            return new FailedValidationResult<string, object?>(token, token, "not found registration");

        var result = await IsValid(pending);

        if (result is FailedValidationResult<PendingRegistration, object?> failedResult)
            return new FailedValidationResult<string, object?>(token, failedResult.WrongValue, failedResult.Message);

        if (result is PassedValidationResult<PendingRegistration>)
            return new PassedValidationResult<string>(token);

        return new FailedValidationResult<string, object?>(token, null, "unknown validation result");
    }

    public async Task<ValidationResult<PendingRegistration>> IsValid(PendingRegistration pending)
    {
        if (pending.ExpiresAt < DateTime.UtcNow)
            return new FailedValidationResult<PendingRegistration, object?>(pending, pending.ExpiresAt, "expired");

        if (!pending.Valid)
            return new FailedValidationResult<PendingRegistration, object?>(pending, pending.Valid, "invalid");

        if (pending.Attempts <= 0)
            return new FailedValidationResult<PendingRegistration, object?>(pending, pending.Attempts, "no attempts left");

        var usernameAlreadyTaken = await _database.Users.AnyAsync(u => u.Username == pending.Username);
        if (usernameAlreadyTaken)
            return new FailedValidationResult<PendingRegistration, object?>(pending, pending.Username, "username already taken");

        var emailAlreadyTaken = await _database.Users.AnyAsync(u => u.Email == pending.Email);
        if (emailAlreadyTaken)
            return new FailedValidationResult<PendingRegistration, object?>(pending, pending.Email, "email already taken");

        return new PassedValidationResult<PendingRegistration>(pending);
    }

    public async Task<bool> ValidateCodeAsync(string token, string code)
    {
        var pending = await _userRepository.GetPendingRegistrationAsync(token)
            ?? throw new NotFoundException("pending registration does not exist");
        
        if (await IsValid(pending) is FailedValidationResult<PendingRegistration, object?>)
            throw new Exception("Invalid pending registration");
        
        var secret = Environment.GetEnvironmentVariable("CODE_HASH_SECRET")
            ?? throw new InvalidOperationException("MISSING ENVIRONMENT VARIABLE: CODE_HASH_SECRET");

        var secretBytes = Convert.FromBase64String(secret);

        byte[] submittedHash = HMACSHA256.HashData(secretBytes, Encoding.UTF8.GetBytes(code));
        return CryptographicOperations.FixedTimeEquals(submittedHash, pending.CodeHash);
    }
}
