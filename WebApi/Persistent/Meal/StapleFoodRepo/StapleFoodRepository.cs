using DomainLibrary.Meal;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Extensions;

namespace WebApi.Persistent.Meal
{
    public class StapleFoodRepository : IStapleFoodRepository
    {
        private readonly FcDbContext _context;

        public StapleFoodRepository(FcDbContext context)
        {
            this._context = context;
        }

        public async Task<bool> IsDuplicateStapleFood(string name)
        {
            return await _context.StapleFoods.AnyAsync(m => m.Name == name);
        }

        public async Task<IEnumerable<StapleFood>> GetStapleFoods()
        {
            return await this._context.StapleFoods.ToListAsync();
        }

        public async Task<StapleFood> GetStapleFood(int id)
        {
            return await _context.StapleFoods.SingleOrDefaultAsync(m => m.Id == id);
        }

        public void AddStapleFood(StapleFood newStapleFood)
        {
            _context.Add(newStapleFood);
        }

        public void Remove(StapleFood existedStapleFood)
        {
            _context.Remove(existedStapleFood);
        }

        public async Task<int> GetNumberOfEntreesWithStapleFood(int StapleFoodId)
        {
            // Use Store Procedure         
            int numberOfEntreeWithVege = -1;

            await _context.LoadStoredProc("dbo.GetNumberOfEntreeByStapleFoodId")
                .WithSqlParam("Id", StapleFoodId)
                .ExecuteStoredProcAsync((handler) =>
                {
                    numberOfEntreeWithVege = handler.ReadToValue<int>() ?? default(int); ;
                    // do something with your results.
                });

            return numberOfEntreeWithVege;
        }
    }
}
