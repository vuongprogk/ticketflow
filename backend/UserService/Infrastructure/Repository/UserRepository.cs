using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;


namespace Infrastructure.Repository;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly ApplicationDbContext _db;
    public UserRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<User?> GetUserByEmailAsync(string query, string email, Func<Task<User>>? task = null)
    {
        async Task<User?> execute()
        {
            using var connection = _db.GetConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email });
        }
        User? result = default;
        if (task is not null)
        {
            try
            {
                result = await task();
            }
            catch (Exception)
            {
                return await execute();
            }
        }
        if (result is not null)
        {
            return result;
        }
        return await execute();
    }

    public async Task<User?> GetUserByIdAsync(string query, string id, Func<Task<User>>? task = null)
    {
        async Task<User?> execute()
        {
            using var connection = _db.GetConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(query, new { Id = id });
        }
        User? result = default;
        if (task is not null)
        {
            try
            {
                result = await task();
            }
            catch (Exception)
            {
                return await execute();
            }
        }
        if (result is not null)
        {
            return result;
        }
        return await execute();
    }
}