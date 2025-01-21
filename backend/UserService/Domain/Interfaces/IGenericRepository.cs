using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<IEnumerable<T>?> GetAllAsync(string query, Func<Task<IEnumerable<T>>>? task = null);
        public Task<T?> GetByIdAsync(string query, string id, Func<Task<T>>? task = null);
        public Task<T?> AddAsync(string query, T obj, Func<Task<T>>? task = null);
        public Task<T?> UpdateAsync(string query, T obj, Func<Task<T>>? task = null);
        public Task<T?> DeleteAsync(string query, T obj, Func<Task<T>>? task = null);
    }
}