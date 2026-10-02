using System.ComponentModel.DataAnnotations;

namespace SchoolHubApi.DTOs;

public class StudentUserDto
{
    [StringLength(30, MinimumLength = 6)]
    [Required]
    public string Username { get; set; } = "";
    
    [StringLength(100, MinimumLength = 8)]
    [Required]
    public string Password { get; set; } = "";
    
    [EmailAddress]
    public string? Email { get; set; }
}
