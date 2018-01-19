using DomainLibrary.Shared;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLibrary.Meal
{
    [Table("EntreeDetailType")]
    public class EntreeDetailType : TransLog
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string DetailType { get; set; }
        [Required]
        [StringLength(20)]
        public string DetailName { get; set; }

        public ICollection<EntreeDetail> EntreeDetails { get; set; }

        public EntreeDetailType()
        {
            EntreeDetails = new Collection<EntreeDetail>();
        }
    }
}
