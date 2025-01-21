using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        public GenericRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<T?> AddAsync(string query, T obj, Func<Task<T>>? task = null)
        {
            async Task<T?> execute()
            {
                using var connection = _db.GetConnection();
                return await connection.QueryFirstOrDefaultAsync<T>(query, obj);
            }
            T? result = default;
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

        public async Task<T?> DeleteAsync(string query, T obj, Func<Task<T>>? task = null)
        {
            async Task<T?> execute()
            {
                using var connection = _db.GetConnection();
                return await connection.QueryFirstOrDefaultAsync<T>(query, obj);
            }
            T? result = default;
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

        public async Task<IEnumerable<T>?> GetAllAsync(string query, Func<Task<IEnumerable<T>>>? task = null)
        {
            async Task<IEnumerable<T>?> execute()
            {
                using var connection = _db.GetConnection();
                return await connection.QueryAsync<T>(query);
            }
            IEnumerable<T>? result = default;
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

        public async Task<T?> GetByIdAsync(string query, string id, Func<Task<T>>? task = null)
        {
            async Task<T?> execute()
            {
                using var connection = _db.GetConnection();
                return await connection.QueryFirstOrDefaultAsync<T>(query, new { Id = id });
            }
            T? result = default;
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

        public async Task<T?> UpdateAsync(string query, T obj, Func<Task<T>>? task = null)
        {
            async Task<T?> execute()
            {
                using var connection = _db.GetConnection();
                return await connection.QueryFirstOrDefaultAsync<T>(query, obj);
            }
            T? result = default;
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
}