using System.Threading.Tasks;

namespace WebApi.Persistent.Utility
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
