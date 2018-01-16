using DomainLibrary.Location;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainLibrary.Shared
{
    [Table("ContactAddress")]
    public class ContactAddress : TransLog
    {
        public int Id { get; set; }
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public int ZipCode { get; set; }
        public Supermarket Supermarket { get; set; }
    }
}
