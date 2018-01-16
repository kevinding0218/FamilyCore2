using System.Threading.Tasks;

namespace WebApi.Persistent.User
{
    public interface IUserRepository
    {
        Task<bool> IsExistedUser(int userId);
        Task<string> GetUserFullName(int userId);
    }
}
