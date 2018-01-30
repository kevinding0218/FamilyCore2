using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Resource.Order;

namespace WebApi.Persistent.Order.CurrentOrder
{
    public interface ICurrentOrderRepository
    {
        Task<DomainLibrary.Order.Order> GetOrder(int OrderId, bool includeMapping = true, bool includeEntree = false);
        Task<DomainLibrary.Order.Order> GetOrderByCurrentDate(DateTime currentDate);
        Task<List<OrderProcessingSingleEntree>> GetCurrentWeekOrderPrepare(DateTime? StartDate, DateTime? EndDate);
        Task<List<OrderEntreeDetailInfo>> GetCurrentWeekOrderEntreeDetails(DateTime? StartDate, DateTime? EndDate);
        void AddOrder(DomainLibrary.Order.Order newOrder);
        void UpdateEntreeOrderMappingScheduleDate(int OrderId, int EntreeId, DateTime ScheduledDate);
    }
}
