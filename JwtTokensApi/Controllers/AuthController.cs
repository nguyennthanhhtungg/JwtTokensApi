using AutoMapper;
using JwtTokensApi.Configurations;
using JwtTokensApi.Exceptions;
using JwtTokensApi.Models;
using JwtTokensApi.Services;
using JwtTokensApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JwtTokensApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtBearerTokenSettings _jwtBearerTokenSettings;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;
        private readonly IJwtService _jwtService;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthController(IOptions<JwtBearerTokenSettings> jwtTokenOptions, IMapper mapper,
            IUserService userService, ILogger<AuthController> logger,
             IJwtService jwtService, IRefreshTokenService refreshTokenService)
        {
            this._jwtBearerTokenSettings = jwtTokenOptions.Value;
            this._mapper = mapper;
            this._userService = userService;
            this._logger = logger;
            this._jwtService = jwtService;
            this._refreshTokenService = refreshTokenService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
        {
            try
            {
                User userMapped = _mapper.Map<User>(registerViewModel);

                User user = await _userService.Register(userMapped, registerViewModel.Password);

                UserViewModel userViewModelMapped = _mapper.Map<UserViewModel>(user);

                return Ok(userViewModelMapped);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            try
            {
                User user = await _userService.ValidateUser(loginViewModel.UserName, loginViewModel.Password);

                if(user == null)
                {
                    throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "Account is invalid!");
                }
                else
                {
                    string accessToken = await _jwtService.GenerateAccessToken(user);

                    RefreshToken refreshToken = _jwtService.GenerateRefreshToken(user);

                    await _refreshTokenService.Add(refreshToken);

                    setRefreshTokenCookie(refreshToken);

                    return Ok(accessToken);
 
                }
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void setRefreshTokenCookie(RefreshToken refreshToken)
        {
            // Append cookie with refresh token to the http response
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true
            };
            HttpContext.Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                string refreshToken = HttpContext.Request.Cookies["refreshToken"];

                if (refreshToken != null)
                {
                    RefreshToken validatedRefreshToken = await _refreshTokenService.ValidateRefreshToken(refreshToken);

                    _logger.LogInformation(JsonConvert.SerializeObject(validatedRefreshToken));

                    if (validatedRefreshToken != null)
                    {
                        User user = await _userService.GetById(validatedRefreshToken.CreatedBy);

                        string accessToken = await _jwtService.GenerateAccessToken(user);

                        return Ok(accessToken);
                    }
                    else
                    {
                        throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "Refresh Token revoked or expired!");
                    }
                }
                else
                {
                    throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "Refresh Token revoked!");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            string refreshToken = HttpContext.Request.Cookies["refreshToken"];

            if(string.IsNullOrEmpty(refreshToken))
            {
                // Revoke Refresh Token 
                RefreshToken revokedRefreshToken = await _refreshTokenService.RevokeRefreshToken(refreshToken);

                // Set Token Cookie
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddDays(-1)
                };

                HttpContext.Response.Cookies.Append("refreshToken", "", cookieOptions);

                return Ok(revokedRefreshToken);
            }

            return Ok();
        }
    }
}
