using JwtTokensApi.Models;
using JwtTokensApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtTokensApi.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            this._roleRepository = roleRepository;
        }

        public async Task<Role> Add(Role role)
        {
            await _roleRepository.Add(role);
            return role;
        }

        public async Task<List<Role>> GetAll()
        {
            var roles = await _roleRepository.GetAll();
            return roles.ToList();
        }

        public async Task<Role> GetById(int id)
        {
            var role = await _roleRepository.GetById(id);
            return role;
        }

        public async Task<Role> Update(Role role)
        {
            await _roleRepository.Update(role);
            return role;
        }

        public async Task<Role> Remove(Role role)
        {
            await _roleRepository.Remove(role);
            return role;
        }
    }
}
