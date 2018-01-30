using System;
using System.Collections.Generic;

namespace WebApi.Resource.Order
{
    public class OrderProcessInfo
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String Note { get; set; }

        public int AddedById { get; set; }
        public DateTime AddedOn { get; set; }
        public int? LastUpdatedById { get; set; }
        public DateTime? LastUpdatedByOn { get; set; }

        // an int array containing all selected entree Ids
        public ICollection<OrderProcessingSingleEntree> EntreeInfoList { get; set; }

    }

    public class OrderProcessingSingleEntree
    {
        public int OrderId { get; set; }
        public int EntreeId { get; set; }
        public string EntreeName { get; set; }
        public string Style { get; set; }
        public string Catagory { get; set; }
        public int? EntreeCount { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string Note { get; set; }
        public string EntreeImgUrl { get; set; }
    }

    public class OrderEntreeDetailInfo
    {
        public string EntreeDetailName { get; set; }
        public int EntreeDetailQty { get; set; }
        public string EntreeDetailTypeName { get; set; }
        public string StapleFood { get; set; }
    }

    public class EntreeOrderMappingSchedule
    {
        public int EntreeId { get; set; }
        public int OrderId { get; set; }
        public DateTime ScheduleDate { get; set; }
    }
}
