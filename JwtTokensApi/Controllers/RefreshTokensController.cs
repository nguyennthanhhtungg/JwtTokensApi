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
    public class RefreshTokensController : ControllerBase
    {
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IMapper _mapper;
        private readonly ILogger<RefreshTokensController> _logger;

        public RefreshTokensController(IRefreshTokenService refreshTokenService, IMapper mapper, ILogger<RefreshTokensController> logger)
        {
            this._refreshTokenService = refreshTokenService;
            this._mapper = mapper;
            this._logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<RefreshToken> refreshTokens = await _refreshTokenService.GetAll();

            List<RefreshTokenViewModel> refreshTokenViewModelListMapped = _mapper.Map<List<RefreshTokenViewModel>>(refreshTokens);

            return Ok(refreshTokenViewModelListMapped);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            RefreshToken refreshToken = await _refreshTokenService.GetById(id);

            RefreshTokenViewModel refreshTokenViewModelMapped = _mapper.Map<RefreshTokenViewModel>(refreshToken);

            return Ok(refreshTokenViewModelMapped);
        }
    }
}
