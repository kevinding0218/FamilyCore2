using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApi.Persistent.Member.JWTAuth
{
    public interface IJwtRepository
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity, bool InternalUser = false);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
        ClaimsIdentity GenerateClaimsIdentityAdmin(string userName, string id);
    }
}
