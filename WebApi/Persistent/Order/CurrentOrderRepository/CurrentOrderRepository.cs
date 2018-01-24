using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

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
