using DomainLibrary.Location;
using DomainLibrary.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLibrary.Meal
{
    [Table("Supermarket_EntreeDetail")]
    public class Supermarket_EntreeDetail : TransLog
    {
        public int SupermarketId { get; set; }
        public int EntreeDetailId { get; set; }
        public string Note { get; set; }
        // one SupermarketToBuyEntreeDetail could have only one Supermarket
        public Supermarket Supermarket { get; set; }
        // one SupermarketToBuyEntreeDetail could have only one EntreeDetail
        public EntreeDetail EntreeDetail { get; set; }
    }
}
