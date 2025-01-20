using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repository;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext db) : base(db)
    {

    }
    public Task<User> AddAsync(User obj)
    {
        throw new NotImplementedException();
    }

    public Task<User> DeleteAsync(User obj)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateAsync(User obj)
    {
        throw new NotImplementedException();
    }


}