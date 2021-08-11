using JwtTokensApi.Models;
using System.Threading.Tasks;

namespace JwtTokensApi.Repositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<bool> IsExistedRoleByRoleId(int roleId);
    }
}
