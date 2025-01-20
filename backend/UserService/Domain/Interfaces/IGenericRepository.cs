using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(string id);
        public Task<T> AddAsync(T obj);
        public Task<T> UpdateAsync(T obj);
        public Task<T> DeleteAsync(T obj);
    }
}