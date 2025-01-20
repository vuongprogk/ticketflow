using Application.Interfaces;
using Domain.Models;

namespace Infrastructure.Services;

public class TokenService: ITokenService
{
    public Task<string> GenerateToken(User user)
    {
        throw new NotImplementedException();
    }
}