using DomainLibrary.Shared;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLibrary.Meal
{
    [Table("EntreeStyle")]
    public class EntreeStyle : TransLog
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Style { get; set; }

        public ICollection<Entree> Entrees { get; set; }

        public EntreeStyle()
        {
            Entrees = new Collection<Entree>();
        }
    }
}
