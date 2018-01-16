using DomainLibrary.Meal;
using System.Threading.Tasks;
using WebApi.Persistent.Query;

namespace WebApi.Persistent.Meal.Vegetable
{
    public interface IVegetableRepository
    {
        Task<bool> IsDuplicateVegetable(string name);
        Task<QueryResult<EntreeDetail>> GetVegetables(VegetableQuery filter);
        Task<EntreeDetail> GetVegetable(int id);
        void AddVegetable(EntreeDetail newVegetable);
        void Remove(EntreeDetail existedVegetable);
        Task<int> GetNumberOfEntreesWithVege(int vegeId);
    }
}
