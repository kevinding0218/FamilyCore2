using DomainLibrary.Meal;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Resource.Shared;

namespace WebApi.Persistent.Meal.EntreeHelperRepo
{
    public interface IEntreeHelperRepository
    {
        Task<List<EntreeStyle>> GetEntreeStyles();
        Task<List<EntreeCatagory>> GetEntreeCatagorys();
        Task<List<StapleFood>> GetStapleFoods();
        Task<List<KeyValuePairResource>> GetAvailableEntreeDetailByType(int currentEntreeId, string entreeDetailType);
    }
}
