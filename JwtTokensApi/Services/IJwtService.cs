using JwtTokensApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtTokensApi.Services
{
    public interface IJwtService
    {
        Task<string> GenerateAccessToken(User user);
        int? ValidateAccessToken(string token);
        RefreshToken GenerateRefreshToken(User user);
    }
}
