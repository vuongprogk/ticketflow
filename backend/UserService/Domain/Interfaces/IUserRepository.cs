using Domain.Models;

namespace Domain.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    //TODO add methods for UserRepository
    Task<User> GetUserByEmailAsync(string email);
    Task<User> GetUserByIdAsync(string id);

}