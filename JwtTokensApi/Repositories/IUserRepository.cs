using JwtTokensApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtTokensApi.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> IsExistedUserByUserName(string userName);

        Task<User> GetUserByUserName(string userName);
    }
}
