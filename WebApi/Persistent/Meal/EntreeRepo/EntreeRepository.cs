using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Extensions;
using WebApi.Resource.Meal.EntreeResource;

namespace WebApi.Persistent.Meal.EntreeRepo
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

        public async Task<IEnumerable<EntreeInfoResource>> GetEntreeDetails()
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


    }
}
