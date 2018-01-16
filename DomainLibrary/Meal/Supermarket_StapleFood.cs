using DomainLibrary.Location;
using DomainLibrary.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLibrary.Meal
{

    [Table("Supermarket_StapleFood")]
    public class Supermarket_StapleFood : TransLog
    {
        public int SuperMarketId { get; set; }
        public int StapleFoodId { get; set; }
        [StringLength(255)]
        public string Note { get; set; }
        // one SupermarketToBuyStapleFood could have only one Entree
        public Supermarket Supermarket { get; set; }
        // one SupermarketToBuyStapleFood could have only one Vegetable
        public StapleFood StapleFood { get; set; }
    }
}
