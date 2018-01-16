using DomainLibrary.Meal;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Extensions;

namespace WebApi.Persistent.Meal.MeatRepo
{
    public class MeatRepository : IMeatRepository
    {
        private readonly FcDbContext _context;

        public MeatRepository(FcDbContext context)
        {
            this._context = context;
        }

        public async Task<bool> IsDuplicateMeat(string name)
        {
            return await _context.EntreeDetails.AnyAsync(m => m.Name == name);
        }

        public async Task<IEnumerable<EntreeDetail>> GetMeats()
        {
            return await this._context.EntreeDetails.Include(ed => ed.EntreeDetailType).Where(ed => ed.EntreeDetailType.DetailType == "肉类").ToListAsync();
        }

        public async Task<EntreeDetail> GetMeat(int id)
        {
            return await _context.EntreeDetails.SingleOrDefaultAsync(m => m.Id == id);
        }

        public void AddMeat(EntreeDetail newMeat)
        {
            _context.Add(newMeat);
        }

        public void Remove(EntreeDetail existedMeat)
        {
            _context.Remove(existedMeat);
        }

        public async Task<int> GetNumberOfEntreesWithMeat(int meatId)
        {
            // Use Store Procedure         
            int numberOfEntreeWithVege = -1;

            await _context.LoadStoredProc("dbo.GetNumberOfEntreeById")
                .WithSqlParam("Id", meatId)
                .ExecuteStoredProcAsync((handler) =>
                {
                    numberOfEntreeWithVege = handler.ReadToValue<int>() ?? default(int); ;
                    // do something with your results.
                });

            return numberOfEntreeWithVege;
        }
    }
}
