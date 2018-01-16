using DomainLibrary.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainLibrary.Location
{
    [Table("Supermarket")]
    public class Supermarket : TransLog
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Note { get; set; }
        public int AddressRefId { get; set; }
        public ContactAddress AddressInfo { get; set; }
    }
}
