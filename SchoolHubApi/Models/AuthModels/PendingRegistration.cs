
using SchoolHubApi.Models.UserModels;

namespace SchoolHubApi.Models.AuthModels;

public class PendingRegistration
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public UserRole Role { get; set; } = UserRole.Student;
    public string Token { get; set; } = "";
    public byte[] CodeHash { get; set; } = [];
    public string Email { get; set; } = "";
    public string Username { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddMinutes(3);
    public bool Valid { get; set; } = true;
    public int Attempts { get; set; } = 3;
}
