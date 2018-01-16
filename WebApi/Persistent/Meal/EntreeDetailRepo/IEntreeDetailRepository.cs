using DomainLibrary.Meal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Persistent.Meal
{
    public interface IEntreeDetailRepository
    {
        Task<bool> IsDuplicateEntreeDetail(string name);
        Task<IEnumerable<EntreeDetail>> GetEntreeDetails();
        Task<EntreeDetail> GetEntreeDetail(int id);
        void AddEntreeDetail(EntreeDetail newEntreeDetail);
        void Remove(EntreeDetail existedEntreeDetail);
        Task<int> GetNumberOfEntreesWithEntreeDetail(int EntreeDetailId);
    }
}
