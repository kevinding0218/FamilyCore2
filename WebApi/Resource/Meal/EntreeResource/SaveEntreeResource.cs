using System;
using WebApi.Resource.Shared;

namespace WebApi.Resource.Meal.EntreeResource
{
    public class SaveEntreeResource
    {
        public KeyValuePairResource keyValuePairInfo { get; set; }
        public DateTime AddedOn { get; set; }

        public DateTime? LastUpdatedByOn { get; set; }
        public int AddedByUserId { get; set; }

        public int? LastUpdatedByUserId { get; set; }
        public string Note { get; set; }
    }
}
