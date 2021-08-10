using JwtTokensApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtTokensApi.Services
{
    public interface IUserService
    {
        Task<User> GetById(int id);

        Task<List<User>> GetAll();

        Task<User> Add(User user);

        Task<User> Update(User user);

        Task<User> Remove(User user);

        Task<User> Register(User user, string password);

        Task<User> ValidateUser(string userName, string password);
    }
}
