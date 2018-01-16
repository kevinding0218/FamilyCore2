using System;
using WebApi.Resource.Shared;

namespace WebApi.Resource.Meal.EntreeResource
{
    public class SaveEntreeDetailResource
    {
        public KeyValuePairResource keyValuePairInfo { get; set; }
        public DateTime AddedOn { get; set; }

        public DateTime? LastUpdatedByOn { get; set; }
        public int AddedById { get; set; }

        public int? LastUpdatedById { get; set; }
        public string Note { get; set; }
    }
}
