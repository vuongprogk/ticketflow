using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRoleRepository
    {
        public Task<bool> GrantedDefaultRole(string userId);
        public Task<bool> AssignRole(string userId, string roleName);
        public Task<bool> RevokeRole(string userId, string roleName);
    }
}