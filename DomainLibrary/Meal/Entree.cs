using DomainLibrary.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLibrary.Meal
{
    [Table("Entree")]
    public class Entree : TransLog
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int? StapleFoodId { get; set; }
        public String Note { get; set; }
        public int EntreeCatagoryId { get; set; }
        public int EntreeStyleId { get; set; }
        public int? CurrentRank { get; set; }

        // one Entree could have only one or null StapleFood
        public virtual StapleFood StapleFood { get; set; }
        // one Entree could have only one EntreeCatagory
        public EntreeCatagory EntreeCatagory { get; set; }
        // one Entree could have only one EntreeStyleId
        public EntreeStyle EntreeStyle { get; set; }
        // one Entree could have many EntreeVegetable
        public ICollection<EntreeDetail> DetailsWithCurrentEntree { get; set; }

        public Entree()
        {
            DetailsWithCurrentEntree = new Collection<EntreeDetail>();
        }
    }
}
