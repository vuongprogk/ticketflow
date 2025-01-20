using Domain.Interfaces;
using Domain.Models;

namespace Infrastructure.Repository;

public class UserRepository: IUserRepository
{
    public Task<User> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }
}