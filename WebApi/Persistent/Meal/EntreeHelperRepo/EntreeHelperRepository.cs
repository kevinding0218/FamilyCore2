using DomainLibrary.Meal;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Extensions;
using WebApi.Resource.Shared;

namespace WebApi.Persistent.Meal.EntreeHelperRepo
{
    public class EntreeHelperRepository : IEntreeHelperRepository
    {
        private readonly FcDbContext _context;

        public EntreeHelperRepository(FcDbContext context)
        {
            this._context = context;
        }

        #region Create / Update Entree Helper
        public async Task<List<EntreeStyle>> GetEntreeStyles()
        {
            return await _context.EntreeStyles.ToListAsync();
        }

        public async Task<List<EntreeCatagory>> GetEntreeCatagorys()
        {
            return await _context.EntreeCatagorys.ToListAsync();
        }

        public async Task<List<StapleFood>> GetStapleFoods()
        {
            return await _context.StapleFoods.ToListAsync();
        }
        #endregion

        #region Entree Detail Cart
        public async Task<List<KeyValuePairResource>> GetAvailableEntreeDetailByType(string entreeDetailType, int currentEntreeId)
        {
            var availableEntreeDetails = new List<KeyValuePairResource>();

            await _context.LoadStoredProc("dbo.GetAvailableEntreeDetailByDetailType")
                .WithSqlParam("Id", currentEntreeId)
                .WithSqlParam("EntreeDetailType", entreeDetailType)
                .ExecuteStoredProcAsync((handler) =>
                {
                    availableEntreeDetails = handler.ReadToList<KeyValuePairResource>().ToList();
                    // do something with your results.
                });

            return availableEntreeDetails;
        }

        public async Task<List<EntreeDetailType>> GetEntreeDetailTypes()
        {
            return await this._context.EntreeDetailTypes.ToListAsync();
        }
        #endregion

        #region Entree Validation
        public async Task<List<KeyValuePairResource>> GetSimilarEntreeList(string entreeName, int stapleFoodId, string EntreeDetailIdList)
        {
            var similarEntreeList = new List<KeyValuePairResource>();

            await _context.LoadStoredProc("dbo.GetSimilarEntreeList")
                .WithSqlParam("entreeName", entreeName)
                .WithSqlParam("stapleFoodId", stapleFoodId)
                .WithSqlParam("entreeDetailIdList", EntreeDetailIdList)
                .ExecuteStoredProcAsync((handler) =>
                {
                    similarEntreeList = handler.ReadToList<KeyValuePairResource>().ToList();
                    // do something with your results.
                });

            return similarEntreeList;
        }
        #endregion

        #region Entree List Main Page
        public async Task<string> GetEntreeStyleOrCatagoryById(string splitBy, int splitId)
        {
            if (splitBy.ToLower().Equals("style"))
            {
                var selectedStyle = await this._context.EntreeStyles.SingleOrDefaultAsync(es => es.Id == splitId);
                return selectedStyle.Style;
            }
            else if (splitBy.ToLower().Equals("catagory"))
            {
                var selectedStyle = await this._context.EntreeCatagorys.SingleOrDefaultAsync(es => es.Id == splitId);
                return selectedStyle.Catagory;
            }
            else
            {
                return "NotFound";
            }

        }
        #endregion
    }
}
