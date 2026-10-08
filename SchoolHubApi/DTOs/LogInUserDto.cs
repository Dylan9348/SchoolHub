
using System.ComponentModel.DataAnnotations;

namespace SchoolHubApi.DTOs;

public class LogInUserDto
{
    [Required]
    public string Username { get; set; } = "";

    [Required]
    public string Password { get; set; } = "";
}
