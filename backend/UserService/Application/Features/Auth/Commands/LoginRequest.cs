namespace Application.Features.Auth.Commands;

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}