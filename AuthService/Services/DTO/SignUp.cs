using System.ComponentModel.DataAnnotations;

namespace AuthService.Services.DTO;

public class SignUp
{
    [Required] public string Login { get; set; } = null!;
    [Required] public string Password { get; set; } = null!;
    [Required] public string Name { get; set; } = null!;
    [Required] public string Email { get; set; } = null!;
}