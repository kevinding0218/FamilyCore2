using System;
using System.Collections.Generic;

namespace WebApi.Resource.Order
{
    public class SaveInitialOrder
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
        public ICollection<EntreeOrderMapping> EntreeOrderMappingsWithCurrentOrder { get; set; }

    }

    public class EntreeOrderMapping
    {
        public int EntreeId { get; set; }
        public int? Count { get; set; }
    }
}
