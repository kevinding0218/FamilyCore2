using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Extensions;
using WebApi.Resource.Order;

namespace WebApi.Persistent.Order.CurrentOrder
{
    public class CurrentOrderRepository : ICurrentOrderRepository
    {
        private readonly FcDbContext _context;

        public CurrentOrderRepository(FcDbContext context)
        {
            this._context = context;
        }

        #region Read Single Order
        public async Task<DomainLibrary.Order.Order> GetOrder(int OrderId, bool includeMapping = true, bool includeEntree = false)
        {
            if (!includeMapping && !includeEntree)
                return await this._context.Orders.SingleOrDefaultAsync(o => o.Id == OrderId);
            else if (includeEntree)
                return await this._context.Orders
                                .Include(o => o.MappingEntreesWithCurrentOrder)
                                    .ThenInclude(e => e.Entree)
                                .SingleOrDefaultAsync(o => o.Id == OrderId);
            else
                return await this._context.Orders
                                .Include(o => o.MappingEntreesWithCurrentOrder)
                                .SingleOrDefaultAsync(o => o.Id == OrderId);
        }

        public async Task<DomainLibrary.Order.Order> GetOrderByCurrentDate(DateTime currentDate)
        {
            if (this._context.Orders.Any(o => o.StartDate.Date <= currentDate.Date && o.EndDate.Date >= currentDate.Date))
            {
                return await this._context.Orders
                                        .Include(o => o.MappingEntreesWithCurrentOrder)
                                        .SingleOrDefaultAsync(o => o.StartDate <= currentDate && o.EndDate >= currentDate);
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Read List
        public async Task<List<OrderProcessingSingleEntree>> GetCurrentWeekOrderPrepare(DateTime? StartDate, DateTime? EndDate)
        {
            var currentWeekEntreeInfoList = new List<OrderProcessingSingleEntree>();
            await _context.LoadStoredProc("dbo.GetWeekOrderPrepare")
                .WithSqlParam("StartDate", StartDate)
                .WithSqlParam("EndDate", EndDate)
                .ExecuteStoredProcAsync((handler) =>
                {
                    currentWeekEntreeInfoList = handler.ReadToList<OrderProcessingSingleEntree>().ToList();
                    // do something with your results.
                });
            return currentWeekEntreeInfoList;
        }

        public async Task<List<OrderEntreeDetailInfo>> GetCurrentWeekOrderEntreeDetails(DateTime? StartDate, DateTime? EndDate)
        {
            var currentWeekEntreeInfoList = new List<OrderEntreeDetailInfo>();
            await _context.LoadStoredProc("dbo.GetWeekOrderEntreeDetails")
                .WithSqlParam("StartDate", StartDate)
                .WithSqlParam("EndDate", EndDate)
                .ExecuteStoredProcAsync((handler) =>
                {
                    currentWeekEntreeInfoList = handler.ReadToList<OrderEntreeDetailInfo>().ToList();
                    // do something with your results.
                });
            return currentWeekEntreeInfoList;
        }
        #endregion

        #region Create Order
        public void AddOrder(DomainLibrary.Order.Order newOrder)
        {
            _context.Add(newOrder);
        }
        #endregion

        #region Update Entree Order Mapping
        public void UpdateEntreeOrderMappingScheduleDate(int OrderId, int EntreeId, DateTime ScheduledDate)
        {
            _context.LoadStoredProc("dbo.UpdateEntreeOrderMappingScheduleDate")
                .WithSqlParam("OrderId", OrderId)
                .WithSqlParam("EntreeId", EntreeId)
                .WithSqlParam("ScheduleDate", ScheduledDate)
                .ExecuteStoredProc((handler) =>
                {
                    // do nothing.
                });
        }
        #endregion
    }
}
