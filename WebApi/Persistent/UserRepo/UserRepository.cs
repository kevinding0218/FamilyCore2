using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WebApi.Persistent.User
{
    public class UserRepository : IUserRepository
    {
        private readonly FcDbContext _context;
        public UserRepository(FcDbContext context)
        {
            this._context = context;

        }
        public async Task<bool> IsExistedUser(int userId)
        {
            return await _context.Users.AnyAsync(u => u.UserID == userId);
        }

        public async Task<string> GetUserFullName(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == userId);

            if (user != null)
            {
                return user.FullName;
            }
            else
            {
                return "User Not Found";
            }
        }
    }
}
