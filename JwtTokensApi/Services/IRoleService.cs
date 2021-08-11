using JwtTokensApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtTokensApi.Services
{
    public interface IRoleService
    {
        Task<Role> GetById(int id);

        Task<List<Role>> GetAll();

        Task<Role> Add(Role role);

        Task<Role> Update(Role role);

        Task<Role> Remove(Role role);
    }
}
