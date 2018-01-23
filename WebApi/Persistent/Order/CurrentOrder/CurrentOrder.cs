using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace WebApi.Persistent.Order.CurrentOrder
{
    public class CurrentOrder : ICurrentOrder
    {
        private readonly FcDbContext _context;

        public CurrentOrder(FcDbContext context)
        {
            this._context = context;
        }

        #region Read Single Order
        public async Task<DomainLibrary.Order.Order> GetOrder(int OrderId, bool includeRelated = true)
        {
            if (!includeRelated)
                return await this._context.Orders.SingleOrDefaultAsync(o => o.Id == OrderId);

            return await this._context.Orders
                                .Include(o => o.MappingEntreesWithCurrentOrder)
                                    .ThenInclude(e => e.Entree)
                                .SingleOrDefaultAsync(o => o.Id == OrderId);
        }

        public async Task<DomainLibrary.Order.Order> GetOrderByCurrentDate(DateTime currentDate)
        {
            return await this._context.Orders
                                        .Include(o => o.MappingEntreesWithCurrentOrder)
                                        .FirstOrDefaultAsync(o => o.StartDate <= currentDate && o.EndDate >= currentDate);
        }
        #endregion

        #region Create Order
        public void AddOrder(DomainLibrary.Order.Order newOrder)
        {
            _context.Add(newOrder);
        }
        #endregion
    }
}
