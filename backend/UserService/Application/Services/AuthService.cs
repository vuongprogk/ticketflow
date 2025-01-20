using Application.Features.Auth.Commands;
using Application.Interfaces;

namespace Application.Services;

public class AuthService: IAuthService
{
    public Task<AuthResult> LoginAsync(LoginRequest request)
    {
        throw new NotImplementedException();
    }
}