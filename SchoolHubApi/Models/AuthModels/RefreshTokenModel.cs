namespace SchoolHubApi.Models.AuthModels;

public class RefreshToken
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Token { get; set; } = "";
    public string UserRole { get; set; } = "";
    public Guid UserId { get; set; }
    public DateTime Expiration { get; set; }
}
