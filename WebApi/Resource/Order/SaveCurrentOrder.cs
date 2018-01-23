using System;
using System.Collections.Generic;

namespace WebApi.Resource.Order
{
    public class SaveCurrentOrder
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
        public ICollection<int> MappingEntreeIdsWithCurrentOrder { get; set; }

    }
}
