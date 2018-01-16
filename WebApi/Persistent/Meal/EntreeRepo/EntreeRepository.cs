using DomainLibrary.Meal;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Extensions;
using WebApi.Resource.Meal.EntreeResource;

namespace WebApi.Persistent.Meal
{
    public class EntreeRepository : IEntreeRepository
    {
        private readonly FcDbContext _context;

        public EntreeRepository(FcDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<EntreeInfoResource>> GetEntreeInfoWithVegeId(int VegeId)
        {
            var entreeInfo = new List<EntreeInfoResource>();
            await _context.LoadStoredProc("dbo.GetEntreeInfoById")
                .WithSqlParam("Id", VegeId)
                .WithSqlParam("Type", "Vegetable")
                .ExecuteStoredProcAsync((handler) =>
                {
                    entreeInfo = handler.ReadToList<EntreeInfoResource>().ToList();
                    // do something with your results.
                });
            return entreeInfo;
        }

        public async Task<IEnumerable<EntreeInfoResource>> GetEntreeInfoWithMeatId(int MeatId)
        {
            var entreeInfo = new List<EntreeInfoResource>();
            await _context.LoadStoredProc("dbo.GetEntreeInfoById")
                .WithSqlParam("Id", MeatId)
                .WithSqlParam("Type", "Meat")
                .ExecuteStoredProcAsync((handler) =>
                {
                    entreeInfo = handler.ReadToList<EntreeInfoResource>().ToList();
                    // do something with your results.
                });
            return entreeInfo;
        }

        public async Task<IEnumerable<EntreeInfoResource>> GetEntreeInfoWithStapleFoodId(int StapleFoodId)
        {
            var entreeInfo = new List<EntreeInfoResource>();
            await _context.LoadStoredProc("dbo.GetEntreeInfoById")
                .WithSqlParam("Id", StapleFoodId)
                .WithSqlParam("Type", "StapleFood")
                .ExecuteStoredProcAsync((handler) =>
                {
                    entreeInfo = handler.ReadToList<EntreeInfoResource>().ToList();
                    // do something with your results.
                });
            return entreeInfo;
        }

        public async Task<IEnumerable<EntreeInfoResource>> GetEntreesList()
        {
            var entreeInfo = new List<EntreeInfoResource>();
            await _context.LoadStoredProc("dbo.GetEntreeInfoById")
                .WithSqlParam("Id", 0)
                .WithSqlParam("Type", "Entree")
                .ExecuteStoredProcAsync((handler) =>
                {
                    entreeInfo = handler.ReadToList<EntreeInfoResource>().ToList();
                    // do something with your results.
                });
            return entreeInfo;
        }

        public async Task<IEnumerable<EntreeDetailResource>> GetEntreeDetailWithEntreeId(int EntreeId)
        {
            var entreeDetails = new List<EntreeDetailResource>();
            await _context.LoadStoredProc("dbo.GetEntreeInfoById")
                .WithSqlParam("Id", EntreeId)
                .WithSqlParam("Type", "EntreeDetail")
                .ExecuteStoredProcAsync((handler) =>
                {
                    entreeDetails = handler.ReadToList<EntreeDetailResource>().ToList();
                    // do something with your results.
                });
            return entreeDetails;
        }

        public async Task<bool> IsDuplicateEntree(string name, int? Id = null)
        {
            if (Id.HasValue)
                return await _context.Entrees.AnyAsync(e => e.Name == name && e.Id != Id);
            return await _context.Entrees.AnyAsync(e => e.Name == name);
        }

        public async Task<IEnumerable<Entree>> GetEntrees()
        {
            return await this._context.Entrees
                .Include(e => e.StapleFood)
                .Include(e => e.EntreeStyle)
                .Include(e => e.EntreeCatagory)
                .ToListAsync();
        }

        public async Task<Entree> GetEntree(int id)
        {
            return await this._context.Entrees
                .Include(e => e.StapleFood)
                .Include(e => e.EntreeStyle)
                .Include(e => e.EntreeCatagory)
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public void AddEntree(Entree newEntree)
        {
            _context.Add(newEntree);
        }

        public void Remove(Entree existedEntree)
        {
            _context.Remove(existedEntree);
        }

        //public async Task<int> GetNumberOfEntreeDetailsWithCurrentEntree(int EntreeId)
        //{
        //    //return await this._context.Entrees.Include(e => e.MappingDetailsWithCurrentEntree).ThenInclude(map => map.en)
        //}
    }
}
