using Application.Features.Auth.Commands;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    public AuthController(IAuthService authService)
    {
        _auth = authService;
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var result = await _auth.LoginAsync(loginRequest);
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest){
        var result = await _auth.RegisterAsync(registerRequest);
        return Ok(result);
    }
}