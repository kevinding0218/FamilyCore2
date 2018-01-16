using DomainLibrary.Shared;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLibrary.Meal
{
    [Table("EntreeCatagory")]
    public class EntreeCatagory : TransLog
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Catagory { get; set; }

        public ICollection<Entree> Entrees { get; set; }

        public EntreeCatagory()
        {
            Entrees = new Collection<Entree>();
        }
    }
}
