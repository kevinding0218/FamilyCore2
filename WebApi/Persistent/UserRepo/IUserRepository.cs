using System.Threading.Tasks;

namespace WebApi.Persistent.User
{
    public interface IUserRepository
    {
        Task<bool> IsExistedUser(int userId);
        Task<string> GetUserFullName(int userId);
        Task<DomainLibrary.Member.User> GetUserById(int userId);
        Task<DomainLibrary.Member.User> GetUserByEmail(string userEmail);
        void RegisterNewUser(DomainLibrary.Member.User newUser);
        Task<bool> VerifyLogin(string userEmail, string unecryptPassword);
    }
}
