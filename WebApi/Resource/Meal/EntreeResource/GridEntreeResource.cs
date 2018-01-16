using System;
using System.Collections.Generic;
using WebApi.Resource.Shared;

namespace WebApi.Resource.Meal.EntreeResource
{
    public class GridEntreeResource
    {
        public KeyValuePairResource keyValuePairInfo { get; set; }
        public String AddedOn { get; set; }

        public String LastUpdatedByOn { get; set; }

        public int AddedById { get; set; }
        public string AddedByUserName { get; set; }

        public int NumberOfEntreeIncluded { get; set; }
        public IEnumerable<EntreeInfoResource> EntreesIncluded { get; set; }
        public string Note { get; set; }
    }
}
