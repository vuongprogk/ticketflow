using Domain.Models;

namespace Domain.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    //TODO add methods for UserRepository
    Task<User?> GetUserByEmailAsync(string query, string email, Func<Task<User>>? task = null);
    Task<User?> GetUserByIdAsync(string query, string id, Func<Task<User>>? task = null);

}