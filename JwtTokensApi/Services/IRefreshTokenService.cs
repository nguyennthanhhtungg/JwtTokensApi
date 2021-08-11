using JwtTokensApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtTokensApi.Services
{
    public interface IRefreshTokenService
    {
        Task<RefreshToken> GetById(int id);

        Task<List<RefreshToken>> GetAll();

        Task<RefreshToken> Add(RefreshToken refreshToken);

        Task<RefreshToken> Update(RefreshToken refreshToken);

        Task<RefreshToken> Remove(RefreshToken refreshToken);

        Task<RefreshToken> RevokeRefreshToken(string token);

        Task<RefreshToken> ValidateRefreshToken(string token);
    }
}
