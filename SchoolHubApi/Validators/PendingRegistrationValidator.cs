
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SchoolHubApi.Data;
using SchoolHubApi.Models.AuthModels;
using SchoolHubApi.Models.Exceptions;
using SchoolHubApi.Repositories;

namespace SchoolHubApi.Validators;

public class PendingRegistrationValidator(Context database, IUserRepository userRepository)
{
    private readonly Context _database = database;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<bool> IsValid(string token)
    {
        var pending = await _database.PendingRegistrations.FirstOrDefaultAsync(r => r.Token == token);
        
        if (pending is null)
            return false;
        
        return await IsValid(pending);
    }

    public async Task<bool> IsValid(PendingRegistration pending)
    {
        var usernameAlreadyTaken = await _database.Users.AnyAsync(u => u.Username == pending.Username);
        var emailAlreadyTaken = await _database.Users.AnyAsync(u => u.Email == pending.Email);

        if (pending.ExpiresAt < DateTime.UtcNow || !pending.Valid || pending.Attempts <= 0 || usernameAlreadyTaken || emailAlreadyTaken)
            return false;
        
        return true;
    }

    public async Task<bool> ValidateCodeAsync(string token, string code)
    {
        var pending = await _userRepository.GetPendingRegistrationAsync(token)
            ?? throw new NotFoundException("pending registration does not exist");
        
        if (!await IsValid(pending))
            throw new Exception("Invalid pending registration");
        
        var secret = Environment.GetEnvironmentVariable("CODE_HASH_SECRET")
            ?? throw new InvalidOperationException("MISSING ENVIRONMENT VARIABLE: CODE_HASH_SECRET");

        var secretBytes = Convert.FromBase64String(secret);

        byte[] submittedHash = HMACSHA256.HashData(secretBytes, Encoding.UTF8.GetBytes(code));
        return CryptographicOperations.FixedTimeEquals(submittedHash, pending.CodeHash);
    }
}
