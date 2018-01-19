using System;
using System.Collections.Generic;

namespace WebApi.Resource.Meal.EntreeResource
{
    public class SaveEntreeResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? StapleFoodId { get; set; }
        public String Note { get; set; }
        public int EntreeCatagoryId { get; set; }
        public int EntreeStyleId { get; set; }
        public int? CurrentRank { get; set; }
        public int AddedById { get; set; }
        public DateTime AddedOn { get; set; }
        public int? LastUpdatedById { get; set; }
        public DateTime? LastUpdatedByOn { get; set; }
        public ICollection<EntreeDetailMappingResource> EntreeDetails { get; set; }
    }
}
