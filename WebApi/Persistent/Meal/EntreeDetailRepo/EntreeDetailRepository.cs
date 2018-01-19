using DomainLibrary.Meal;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Extensions;
using WebApi.Resource.Meal.EntreeResource;

namespace WebApi.Persistent.Meal
{
    public class EntreeDetailRepository : IEntreeDetailRepository
    {
        private readonly FcDbContext _context;

        public EntreeDetailRepository(FcDbContext context)
        {
            this._context = context;
        }

        public async Task<bool> IsDuplicateEntreeDetail(string name, int? Id = null)
        {
            if (Id.HasValue)
                return await _context.EntreeDetails.AnyAsync(ed => ed.Name == name && ed.Id != Id);
            return await _context.EntreeDetails.AnyAsync(ed => ed.Name == name);
        }

        public async Task<IEnumerable<EntreeDetail>> GetEntreeDetails(string EntreeDetailType)
        {
            return await this._context.EntreeDetails.Include(ed => ed.EntreeDetailType).Where(ed => ed.EntreeDetailType.DetailName.ToLower() == EntreeDetailType.ToLower()).ToListAsync();
        }

        public async Task<EntreeDetail> GetEntreeDetail(int id)
        {
            return await _context.EntreeDetails
                            .Include(ed => ed.EntreeDetailType)
                            .SingleOrDefaultAsync(m => m.Id == id);
        }

        public void AddEntreeDetail(EntreeDetail newEntreeDetail)
        {
            _context.Add(newEntreeDetail);
        }

        public void Remove(EntreeDetail existedEntreeDetail)
        {
            _context.Remove(existedEntreeDetail);
        }

        public async Task<IEnumerable<EntreeInfoResource>> GetEntreeInfoWithEntreeDetailId(int EntreeDetailId)
        {
            var entreeInfo = new List<EntreeInfoResource>();
            await _context.LoadStoredProc("dbo.GetEntreeInfoById")
                .WithSqlParam("Id", EntreeDetailId)
                .WithSqlParam("Type", "EntreeDetail")
                .ExecuteStoredProcAsync((handler) =>
                {
                    entreeInfo = handler.ReadToList<EntreeInfoResource>().ToList();
                    // do something with your results.
                });
            return entreeInfo;
        }

        public async Task<int> GetNumberOfEntreesWithEntreeDetail(int EntreeDetailId)
        {
            // Use Store Procedure         
            int numberOfEntreeWithVege = -1;

            await _context.LoadStoredProc("dbo.GetNumberOfEntreeByEntreeDetailId")
                .WithSqlParam("Id", EntreeDetailId)
                .ExecuteStoredProcAsync((handler) =>
                {
                    numberOfEntreeWithVege = handler.ReadToValue<int>() ?? default(int); ;
                    // do something with your results.
                });

            return numberOfEntreeWithVege;
        }

        public async Task<int> GetEntreeDetailTypeIdByType(string detailType)
        {
            var entreeDetailType = await _context.EntreeDetailTypes.SingleOrDefaultAsync(edt => edt.DetailName.ToLower().Equals(detailType.ToLower()));
            return entreeDetailType.Id;
        }
    }
}
