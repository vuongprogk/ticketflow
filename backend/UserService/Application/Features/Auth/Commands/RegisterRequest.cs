using System.ComponentModel.DataAnnotations;

namespace Application.Features.Auth.Commands;

public class RegisterRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    [MinLength(8, ErrorMessage = "Your password must be longer than 8 characters.")]
    public string Password { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;


}