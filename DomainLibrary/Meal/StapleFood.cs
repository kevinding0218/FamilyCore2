using DomainLibrary.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLibrary.Meal
{
    [Table("StapleFood")]
    public class StapleFood : TransLog
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        //public int MealTypeId { get; set; }

        [StringLength(255)]
        public string Note { get; set; }

        // one StapleFood could have only one MealType 4
        //public MealType MealType { get; set; }
        // one StapleFood could have only many Entree
        public ICollection<Entree> EntreesWithCurrentStapleFood { get; set; }
    }
}
