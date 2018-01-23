using DomainLibrary.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLibrary.Meal
{
    [Table("Order")]
    public class Order : TransLog
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String Note { get; set; }

        // one Order could have many Entrees_Orders
        public ICollection<Entrees_Orders> MappingEntreesWithCurrentOrder { get; set; }

        public Order()
        {
            MappingEntreesWithCurrentOrder = new Collection<Entrees_Orders>();
        }
    }
}
