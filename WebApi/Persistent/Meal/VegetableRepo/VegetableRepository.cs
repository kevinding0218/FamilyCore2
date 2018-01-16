using DomainLibrary.Meal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApi.Extensions;
using WebApi.Persistent.Query;

namespace WebApi.Persistent.Meal
{
    public class VegetableRepository : IVegetableRepository
    {
        private readonly FcDbContext _context;

        public VegetableRepository(FcDbContext context)
        {
            this._context = context;
        }

        public async Task<bool> IsDuplicateVegetable(string name, int? Id = null)
        {
            if (Id.HasValue)
                return await _context.EntreeDetails.AnyAsync(ed => ed.Name == name && ed.Id != Id);
            return await _context.EntreeDetails.AnyAsync(ed => ed.Name == name);
        }

        public async Task<EntreeDetail> GetVegetableWithSameName(string name)
        {
            return await _context.EntreeDetails.SingleOrDefaultAsync(ed => ed.Name == name);
        }

        public async Task<EntreeDetail> GetVegetable(int id)
        {
            return await _context.EntreeDetails.SingleOrDefaultAsync(v => v.Id == id);
        }

        public void AddVegetable(EntreeDetail newVegetable)
        {
            _context.Add(newVegetable);
        }

        public void Remove(EntreeDetail existedVegetable)
        {
            _context.Remove(existedVegetable);
        }

        public async Task<QueryResult<EntreeDetail>> GetVegetables(VegetableQuery queryObj)
        {
            var result = new QueryResult<EntreeDetail>();
            var query = _context.EntreeDetails.Include(ed => ed.EntreeDetailType).Where(ed => ed.EntreeDetailType.DetailType == "蔬菜").AsQueryable();

            /*
             if (queryObj.VegetableId.HasValue)
                query = query.Where(vege => vege.Id == queryObj.VegetableId);

            if (queryObj.SortBy == "name")
                query = (queryObj.IsSortAscending) ? query.OrderBy(vege => vege.Name) : query.OrderByDescending(vege => vege.Name); 
            */

            var columnsMap = new Dictionary<string, Expression<Func<EntreeDetail, object>>>()
            {
                ["name"] = v => v.Name,
                ["addedOn"] = v => v.AddedOn,
                ["updatedOn"] = v => v.LastUpdatedByOn
            };
            //columnsMap.Add("make", v => v.Model.Make.Name);
            //query = ApplyOrdering(queryObj, query, columnsMap);
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            //query = query.ApplyPaging(queryObj);

            //return await query.ToListAsync();
            result.TotalItemList = result.Items = await query.ToListAsync();

            return result;
        }

        public async Task<int> GetNumberOfEntreesWithVege(int vegeId)
        {
            // Use Store Procedure         
            int numberOfEntreeWithVege = -1;

            await _context.LoadStoredProc("dbo.GetNumberOfEntreeByEntreeDetailId")
                .WithSqlParam("Id", vegeId)
                .ExecuteStoredProcAsync((handler) =>
                {
                    numberOfEntreeWithVege = handler.ReadToValue<int>() ?? default(int); ;
                    // do something with your results.
                });

            /*  
            // Use Raw SQL
            await _context.LoadSQLText ("SELECT COUNT(*) AS TotalEntrees FROM EntreeVegetable WHERE VegeId = " + vegeId.ToString ())
                 .ExecuteSQLTextAsync ((handler) => {
                     numberOfEntreeWithVege = handler.ReadToValue<int>() ?? default(int);;
                     // do something with your results.
                 }); 
            */

            /*
            // Use Entity Framework Query Data
            // Note: include not working in Count()
             var vegetable = this._context.Vegetables.Single(v => v.Id == vegeId);

             numberOfEntreeWithVege = await this._context.Entry(vegetable)
                .Collection(v => v.EntreesWithCurrentVegetable)
                .Query()
                .CountAsync(); 
            */

            return numberOfEntreeWithVege;
        }
    }
}
