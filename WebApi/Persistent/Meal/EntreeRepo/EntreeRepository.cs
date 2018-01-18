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

        public async Task<IEnumerable<EntreeInfoResource>> GetEntireEntreesList()
        {
            var entreeInfo = new List<EntreeInfoResource>();
            await _context.LoadStoredProc("dbo.GetEntreeInfoBySplit")
                .WithSqlParam("Id", -1)
                .WithSqlParam("SplitBy", "")
                .ExecuteStoredProcAsync((handler) =>
                {
                    entreeInfo = handler.ReadToList<EntreeInfoResource>().ToList();
                    // do something with your results.
                });
            return entreeInfo;
        }

        #region Entree Main Grid
        public async Task<IEnumerable<EntreeInfoResource>> GetSplitEntreesList(string SplitBy, int Id)
        {
            var entreeInfo = new List<EntreeInfoResource>();
            await _context.LoadStoredProc("dbo.GetEntreeInfoBySplit")
                .WithSqlParam("Id", Id)
                .WithSqlParam("SplitBy", SplitBy)
                .ExecuteStoredProcAsync((handler) =>
                {
                    entreeInfo = handler.ReadToList<EntreeInfoResource>().ToList();
                    // do something with your results.
                });
            return entreeInfo;
        }
        #endregion

        #region Entree Detail Grid
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
        #endregion

        public async Task<IEnumerable<Entree>> GetEntrees()
        {
            return await this._context.Entrees
                .Include(e => e.StapleFood)
                .Include(e => e.EntreeStyle)
                .Include(e => e.EntreeCatagory)
                .ToListAsync();
        }

        #region Read Single Entree
        public async Task<Entree> GetEntree(int id)
        {
            return await this._context.Entrees
                .Include(e => e.MappingDetailsWithCurrentEntree)
                .SingleOrDefaultAsync(m => m.Id == id);
        }
        #endregion

        #region Create Entree
        public void AddEntree(Entree newEntree)
        {
            _context.Add(newEntree);
        }
        #endregion

        #region Delete Entree
        public void Remove(Entree existedEntree)
        {
            _context.Remove(existedEntree);
        }
        #endregion

        public async Task<bool> IsDuplicateEntree(string name, int? Id = null)
        {
            if (Id.HasValue)
                return await _context.Entrees.AnyAsync(e => e.Name == name && e.Id != Id);
            return await _context.Entrees.AnyAsync(e => e.Name == name);
        }

        //public async Task<int> GetNumberOfEntreeDetailsWithCurrentEntree(int EntreeId)
        //{
        //    //return await this._context.Entrees.Include(e => e.MappingDetailsWithCurrentEntree).ThenInclude(map => map.en)
        //}
    }
}
