using JwtTokensApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace JwtTokensApi.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(JwtTokensDbContext context) : base(context)
        {

        }


        public async Task<bool> IsExistedUserByUserName(string userName)
        {
            User user = await _context.Set<User>()
                .Where(usr => usr.UserName == userName)
                .FirstOrDefaultAsync();

            if(user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            return await _context.Set<User>()
                .AsNoTracking()
                .Where(usr => usr.UserName == userName)
                .FirstOrDefaultAsync();
        }
    }
}
