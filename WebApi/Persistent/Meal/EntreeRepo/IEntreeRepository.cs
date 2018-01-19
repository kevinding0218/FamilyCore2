using DomainLibrary.Meal;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Resource.Meal.EntreeResource;

namespace WebApi.Persistent.Meal
{
    public interface IEntreeRepository
    {
        Task<IEnumerable<EntreeInfoResource>> GetEntireEntreesList();
        Task<IEnumerable<EntreeInfoResource>> GetSplitEntreesList(string SplitBy, int Id);
        Task<IEnumerable<EntreeDetailResource>> GetEntreeDetailWithEntreeId(int EntreeId);

        Task<bool> IsDuplicateEntree(string name, int? Id = null);
        Task<IEnumerable<Entree>> GetEntrees();
        Task<Entree> GetEntree(int EntreeId, bool includeRelated = true);
        void AddEntree(Entree newEntree);
        void Remove(Entree existedEntree);
    }
}
