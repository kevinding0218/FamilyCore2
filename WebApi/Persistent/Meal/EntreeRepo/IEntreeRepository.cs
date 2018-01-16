using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Resource.Meal.EntreeResource;

namespace WebApi.Persistent.Meal.EntreeRepo
{
    public interface IEntreeRepository
    {
        Task<IEnumerable<EntreeInfoResource>> GetEntreeInfoWithVegeId(int VegeId);
        Task<IEnumerable<EntreeInfoResource>> GetEntreeInfoWithMeatId(int MeatId);
        Task<IEnumerable<EntreeInfoResource>> GetEntreeDetails();
        Task<IEnumerable<EntreeDetailResource>> GetEntreeDetailWithEntreeId(int EntreeId);
    }
}
