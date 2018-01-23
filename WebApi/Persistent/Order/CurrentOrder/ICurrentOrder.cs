using System;
using System.Threading.Tasks;

namespace WebApi.Persistent.Order.CurrentOrder
{
    public interface ICurrentOrder
    {
        Task<DomainLibrary.Order.Order> GetOrder(int OrderId, bool includeRelated = true);
        Task<DomainLibrary.Order.Order> GetOrderByCurrentDate(DateTime currentDate);
        void AddOrder(DomainLibrary.Order.Order newOrder);
    }
}
