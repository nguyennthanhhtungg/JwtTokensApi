using AutoMapper;
using JwtTokensApi.Models;
using JwtTokensApi.Services;
using JwtTokensApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtTokensApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RolesController(IRoleService roleService, IMapper mapper)
        {
            this._roleService = roleService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Role> roles = await _roleService.GetAll();

            List<RoleViewModel> roleViewModelListMapped = _mapper.Map<List<RoleViewModel>>(roles);

            return Ok(roleViewModelListMapped);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Role role = await _roleService.GetById(id);

            RoleViewModel roleViewModelMapped = _mapper.Map<RoleViewModel>(role);

            return Ok(roleViewModelMapped);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateRoleViewModel createRoleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    ErrorMessage = "Role Info is invalid!"
                });
            }

            try
            {
                Role roleMapped = _mapper.Map<Role>(createRoleViewModel);

                await _roleService.Add(roleMapped);

                RoleViewModel roleViewModelMapped = _mapper.Map<RoleViewModel>(roleMapped);

                return Ok(roleViewModelMapped);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e}");
                return BadRequest(new
                {
                    ErrorMessage = "Category Info is invalid!"
                });
            }
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateRoleViewModel updateRoleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    ErrorMessage = "Role Info is invalid!"
                });
            }

            try
            {
                Role roleMapped = _mapper.Map<Role>(updateRoleViewModel);

                await _roleService.Update(roleMapped);

                RoleViewModel roleViewModelMapped = _mapper.Map<RoleViewModel>(roleMapped);

                return Ok(roleViewModelMapped);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e}");
                return BadRequest(new
                {
                    ErrorMessage = "Role Info is invalid!"
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                Role role = await _roleService.GetById(id);

                if(role == null)
                {
                    return BadRequest(new
                    {
                        ErrorMessage = "RoleId is invalid!"
                    });
                }
                else
                {
                    await _roleService.Remove(role);

                    RoleViewModel roleViewModelMapped = _mapper.Map<RoleViewModel>(role);

                    return Ok(roleViewModelMapped);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e}");
                return BadRequest(new
                {
                    ErrorMessage = "RoleId is invalid!"
                });
            }
        }
    }
}
