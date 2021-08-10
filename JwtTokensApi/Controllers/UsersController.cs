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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, IMapper mapper, ILogger<UsersController> logger)
        {
            this._userService = userService;
            this._mapper = mapper;
            this._logger = logger;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<User> users = await _userService.GetAll();

            List<UserViewModel> userViewModelListMapped = _mapper.Map<List<UserViewModel>>(users);

            return Ok(userViewModelListMapped);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            User user = await _userService.GetById(id);

            UserViewModel userViewModelMapped = _mapper.Map<UserViewModel>(user);

            return Ok(userViewModelMapped);
        }
    }
}
