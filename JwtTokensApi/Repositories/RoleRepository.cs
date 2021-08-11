using JwtTokensApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtTokensApi.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(JwtTokensDbContext context) : base(context)
        {
        }

        public async Task<bool> IsExistedRoleByRoleId(int roleId)
        {
            Role role = await _context.Set<Role>()
                                .Where(r => r.RoleId == roleId)
                                .FirstOrDefaultAsync();

            if (role == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
