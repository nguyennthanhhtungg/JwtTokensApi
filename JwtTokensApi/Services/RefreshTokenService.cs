using JwtTokensApi.Models;
using JwtTokensApi.Repositories;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtTokensApi.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            this._refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<RefreshToken> Add(RefreshToken refreshToken)
        {
            await _refreshTokenRepository.Add(refreshToken);

            return refreshToken;
        }

        public async Task<List<RefreshToken>> GetAll()
        {
            List<RefreshToken> refreshTokens = await _refreshTokenRepository.GetAll();

            return refreshTokens.ToList();
        }

        public async Task<RefreshToken> GetById(int id)
        {
            var refreshToken = await _refreshTokenRepository.GetById(id);
            return refreshToken;
        }

        public async Task<RefreshToken> Update(RefreshToken refreshToken)
        {
            await _refreshTokenRepository.Update(refreshToken);
            return refreshToken;
        }

        public async Task<RefreshToken> Remove(RefreshToken refreshToken)
        {
            await _refreshTokenRepository.Remove(refreshToken);
            return refreshToken;
        }


        public async Task<RefreshToken> RevokeRefreshToken(string token)
        {
            return await _refreshTokenRepository.RevokeRefreshToken(token);
        }

        public async Task<RefreshToken> ValidateRefreshToken(string token)
        {
            RefreshToken refreshToken = await _refreshTokenRepository.GetRefreshTokenByToken(token);

            if (refreshToken != null)
            {
                if(refreshToken.RevokedOn != null)
                {
                    return null;
                }


                if (refreshToken.ExpiryOn <= DateTime.UtcNow)
                {
                    refreshToken.RevokedOn = DateTime.UtcNow;

                    await _refreshTokenRepository.Update(refreshToken);

                    return null;
                }
            }

            return refreshToken;
        }
    }
}
