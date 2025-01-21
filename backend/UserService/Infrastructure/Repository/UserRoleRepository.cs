using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRoleRepository(ApplicationDbContext db) { _db = db; }
        public Task<bool> AssignRole(string userId, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GrantedDefaultRole(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RevokeRole(string userId, string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
