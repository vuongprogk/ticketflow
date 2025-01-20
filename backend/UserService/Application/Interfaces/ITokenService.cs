using Domain.Models;

namespace Application.Interfaces;
public interface ITokenService
{
    public Task<string> GenerateToken(User user);
}