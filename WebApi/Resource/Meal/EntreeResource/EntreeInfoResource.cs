using System;
using System.Collections.Generic;
using WebApi.CustomAttribute;

namespace WebApi.Resource.Meal.EntreeResource
{
    public class EntreeInfoResource
    {
        public int EntreeId { get; set; }
        public string EntreeName { get; set; }
        public int VegetableCount { get; set; }
        public int MeatCount { get; set; }
        public string StapleFood { get; set; }
        public string Style { get; set; }
        public string Catagory { get; set; }
        public int Rank { get; set; }
        public string Note { get; set; }

        public int AddedById { get; set; }
        public string AddedByUserName { get; set; }
        public String AddedOn { get; set; }

        [IngoreReadToListAttribute]
        public IEnumerable<EntreeDetailResource> EntreeDetailList { get; set; }

        public EntreeInfoResource()
        {
            EntreeDetailList = new List<EntreeDetailResource>();
        }
    }
}
