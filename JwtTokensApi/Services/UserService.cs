using JwtTokensApi.Exceptions;
using JwtTokensApi.Models;
using JwtTokensApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JwtTokensApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            this._userRepository = userRepository;
            this._roleRepository = roleRepository;
        }

        public async Task<User> Add(User user)
        {
            await _userRepository.Add(user);
            return user;
        }

        public async Task<List<User>> GetAll()
        {
            var users = await _userRepository.GetAll();
            return users.ToList();
        }

        public async Task<User> GetById(int id)
        {
            var user = await _userRepository.GetById(id);
            return user;
        }

        public async Task<User> Update(User user)
        {
            await _userRepository.Update(user);
            return user;
        }

        public async Task<User> Remove(User user)
        {
            await _userRepository.Remove(user);
            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            bool isExistedUser = await _userRepository.IsExistedUserByUserName(user.UserName);

            if(isExistedUser == true)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "UserName has already existed!");
            }

            bool isExistedRole = await _roleRepository.IsExistedRoleByRoleId(user.RoleId);

            if (isExistedRole == false)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "RoleId is invalid!");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            user.PasswordHash = passwordHash;

            await _userRepository.Add(user);

            return user;
        }

        public async Task<User> ValidateUser(string userName, string password)
        {
            User user = await _userRepository.GetUserByUserName(userName);

            if(user != null)
            {
                bool verified = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

                if(verified == false)
                {
                    return null;
                }
            }

            return user;
        }
    }
}
