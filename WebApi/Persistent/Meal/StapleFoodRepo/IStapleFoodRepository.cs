using DomainLibrary.Meal;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Resource.Meal.EntreeResource;

namespace WebApi.Persistent.Meal
{
    public interface IStapleFoodRepository
    {
        Task<bool> IsDuplicateStapleFood(string name);
        Task<IEnumerable<StapleFood>> GetStapleFoods();
        Task<StapleFood> GetStapleFood(int id);
        void AddStapleFood(StapleFood newStapleFood);
        void Remove(StapleFood existedStapleFood);
        Task<IEnumerable<EntreeInfoResource>> GetEntreeInfoWithStapleFoodId(int StapleFoodId);
        Task<int> GetNumberOfEntreesWithStapleFood(int StapleFoodId);
    }
}
