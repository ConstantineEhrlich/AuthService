using System.ComponentModel.DataAnnotations;

namespace AuthService.Services.DTO;

public class SignIn
{
    [Required] public string Login { get; set; } = null!;
    [Required] public string Password { get; set; } = null!;
}