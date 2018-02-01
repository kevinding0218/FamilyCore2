using DomainLibrary.Meal;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Persistent.Meal.EntreePhotoRepo
{
    public class EntreePhotoRepository : IEntreePhotoRepository
    {
        private readonly FcDbContext _context;

        public EntreePhotoRepository(FcDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<EntreePhoto>> GetPhotos(int entreeId)
        {
            return await _context.EntreePhotos
                  .Where(ep => ep.EntreeId == entreeId)
                  .ToListAsync();
        }
    }
}
