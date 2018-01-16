using DomainLibrary.Meal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Persistent.Meal.MeatRepo
{
    public interface IMeatRepository
    {
        Task<bool> IsDuplicateMeat(string name);
        Task<IEnumerable<EntreeDetail>> GetMeats();
        Task<EntreeDetail> GetMeat(int id);
        void AddMeat(EntreeDetail newMeat);
        void Remove(EntreeDetail existedMeat);
        Task<int> GetNumberOfEntreesWithMeat(int meatId);
    }
}
