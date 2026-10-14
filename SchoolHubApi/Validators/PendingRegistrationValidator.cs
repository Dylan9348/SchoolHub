
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SchoolHubApi.Data;
using SchoolHubApi.Models.AuthModels;
using SchoolHubApi.Exceptions;
using SchoolHubApi.Validators.ValidationResults;
using SchoolHubApi.Repositories;
using SchoolHubApi.Enums;

namespace SchoolHubApi.Validators;

public class PendingRegistrationValidator(Context database, IUserRepository userRepository)
{
    private readonly Context _database = database;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ValidationResult> IsValid(string token)
    {
        var pending = await _database.PendingRegistrations.FirstOrDefaultAsync(r => r.Token == token);

        if (pending is null)
            return new FailedValidationResult(FailedValidationType.NotFound, message: "registration not found");

        var result = await IsValid(pending);

        if (result is FailedValidationResult failedResult)
            return new FailedValidationResult(failedResult.Type, failedResult.WrongValue, failedResult.Message);

        return new PassedValidationResult();
    }

    public async Task<ValidationResult> IsValid(PendingRegistration pending)
    {
        if (pending.ExpiresAt < DateTime.UtcNow)
            return new FailedValidationResult(FailedValidationType.Expired, pending.ExpiresAt, "expired");

        if (!pending.Valid)
            return new FailedValidationResult(FailedValidationType.Invalid, pending.Valid, "invalid");

        if (pending.Attempts <= 0)
            return new FailedValidationResult(FailedValidationType.NoAttemptsLeft, pending.Attempts, "no attempts left");

        var usernameAlreadyTaken = await _database.Users.AnyAsync(u => u.Username == pending.Username);
        if (usernameAlreadyTaken)
            return new FailedValidationResult(FailedValidationType.AlreadyTaken, pending.Username, "username already taken");

        var emailAlreadyTaken = await _database.Users.AnyAsync(u => u.Email == pending.Email);
        if (emailAlreadyTaken)
            return new FailedValidationResult(FailedValidationType.AlreadyTaken, pending.Email, "email already taken");

        return new PassedValidationResult();
    }

    public async Task<ValidationResult> ValidateCodeAsync(string token, string code)
    {
        var pending = await _userRepository.GetPendingRegistrationAsync(token)
            ?? throw new NotFoundException("pending registration does not exist");
        
        if (!await IsValid(pending))
            throw new Exception("Invalid pending registration");
        
        var secret = Environment.GetEnvironmentVariable("CODE_HASH_SECRET")
            ?? throw new InvalidOperationException("MISSING ENVIRONMENT VARIABLE: CODE_HASH_SECRET");

        var secretBytes = Convert.FromBase64String(secret);

        byte[] submittedHash = HMACSHA256.HashData(secretBytes, Encoding.UTF8.GetBytes(code));
        
        var isValid = CryptographicOperations.FixedTimeEquals(submittedHash, pending.CodeHash);

        if (isValid)
            return new PassedValidationResult();

        return new FailedValidationResult(FailedValidationType.Invalid);
    }
}
