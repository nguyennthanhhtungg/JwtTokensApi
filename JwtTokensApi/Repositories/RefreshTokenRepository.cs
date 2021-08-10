using JwtTokensApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JwtTokensApi.Repositories
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(JwtTokensDbContext context) : base(context)
        {
        }

        public async Task<RefreshToken> GetRefreshTokenByToken(string token)
        {
            return await _context.Set<RefreshToken>()
                .Where(rfToken => rfToken.Token == token)
                .FirstOrDefaultAsync();
        }

        public async Task<RefreshToken> RevokeRefreshToken(string token)
        {
            RefreshToken revokedRefreshToken = _context.Set<RefreshToken>()
                .Where(c => c.Token == token)
                .FirstOrDefault();

            if(revokedRefreshToken != null)
            {
                revokedRefreshToken.RevokedOn = DateTime.UtcNow;
                await Update(revokedRefreshToken);
            }

            return revokedRefreshToken;
        }
    }
}
