using DomainLibrary.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLibrary.Meal
{
    [Table("EntreeDetail")]
    public class EntreeDetail : TransLog
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int EntreeDetailTypeId { get; set; }
        public string Note { get; set; }


        // one EntreeDetail could have only one MealType
        public EntreeDetailType EntreeDetailType { get; set; }
        // one Vegetable could have many EntreeVegetable
        public ICollection<Entrees_Details> EntreesWithCurrentEntreeWithDetail { get; set; }
    }
}
