using DomainLibrary.Meal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Persistent.Meal
{
    public interface IEntreeDetailRepository
    {
        Task<bool> IsDuplicateEntreeDetail(string name, int? Id = null);
        Task<IEnumerable<EntreeDetail>> GetEntreeDetails(string EntreeDetailType);
        Task<EntreeDetail> GetEntreeDetail(int id);
        void AddEntreeDetail(EntreeDetail newEntreeDetail);
        void Remove(EntreeDetail existedEntreeDetail);
        Task<int> GetNumberOfEntreesWithEntreeDetail(int EntreeDetailId);
        Task<int> GetEntreeDetailTypeIdByType(string detailType);
    }
}
