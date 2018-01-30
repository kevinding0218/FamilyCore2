using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLibrary.Meal
{
    [Table("Entrees_Orders")]
    public class Entrees_Orders
    {
        public int OrderId { get; set; }
        public int EntreeId { get; set; }
        public int? Count { get; set; }
        public string Note { get; set; }
        public DateTime? ScheduledDate { get; set; }

        // one Entrees_Orders could have only one Entree
        public Entree Entree { get; set; }
        // one Entrees_Orders could have only one Order
        public DomainLibrary.Order.Order Order { get; set; }
    }
}
