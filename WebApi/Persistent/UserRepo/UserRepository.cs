using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Persistent.User
{
    public class UserRepository : IUserRepository
    {
        private readonly FcDbContext _context;
        private readonly IPasswordRepository _passwordRepository;
        public UserRepository(FcDbContext context, IPasswordRepository passwordRepository)
        {
            this._context = context;
            this._passwordRepository = passwordRepository;
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

        public async Task<DomainLibrary.Member.User> GetUserById(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserID == userId);
        }

        public async Task<DomainLibrary.Member.User> GetUserByEmail(string userEmail)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
        }

        public void RegisterNewUser(DomainLibrary.Member.User newUser)
        {
            _context.Add(newUser);
        }

        public async Task<bool> VerifyLogin(string userEmail, string unecryptPassword)
        {
            var userFromDB = await _context.Users
                            .Include(u => u.UserPasswords)
                            .FirstOrDefaultAsync(u => u.Email == userEmail);

            var userStoredPassword = userFromDB.UserPasswords.Where(up => up.Active == true).SingleOrDefault().Password;
            return this._passwordRepository.Encrypt(unecryptPassword).Equals(userStoredPassword);
        }
    }
}
