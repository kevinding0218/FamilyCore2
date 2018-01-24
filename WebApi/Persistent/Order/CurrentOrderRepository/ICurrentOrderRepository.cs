using System;
using System.Threading.Tasks;

namespace WebApi.Persistent.Order.CurrentOrder
{
    public interface ICurrentOrderRepository
    {
        Task<DomainLibrary.Order.Order> GetOrder(int OrderId, bool includeMapping = true, bool includeEntree = false);
        Task<DomainLibrary.Order.Order> GetOrderByCurrentDate(DateTime currentDate);
        void AddOrder(DomainLibrary.Order.Order newOrder);
    }
}
