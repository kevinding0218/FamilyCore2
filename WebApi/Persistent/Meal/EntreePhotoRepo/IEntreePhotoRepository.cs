using DomainLibrary.Meal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Persistent.Meal.EntreePhotoRepo
{
    public interface IEntreePhotoRepository
    {
        Task<IEnumerable<EntreePhoto>> GetPhotos(int entreeId);
    }
}
