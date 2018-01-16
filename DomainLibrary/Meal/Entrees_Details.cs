using DomainLibrary.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLibrary.Meal
{
    [Table("Entrees_Details")]
    public class Entrees_Details : TransLog
    {
        public int EntreeId { get; set; }
        public int EntreeDetailId { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }


        // one EntreeWithDetails could have only one Entree
        public Entree Entree { get; set; }
        // one EntreeWithDetails could have only one EntreeDetail
        public EntreeDetail EntreeDetail { get; set; }
    }
}
