using JwtTokensApi.Models;
using System.Threading.Tasks;

namespace JwtTokensApi.Repositories
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
    {
        Task<RefreshToken> GetRefreshTokenByToken(string token);
        Task<RefreshToken> RevokeRefreshToken(string token);
    }
}
