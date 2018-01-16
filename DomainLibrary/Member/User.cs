using DomainLibrary.Shared;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLibrary.Member
{
    [Table("Users")]
    public class User : TransLog
    {
        public int UserID { get; set; }

        [Required]
        [StringLength(100)]
        [Column(Order = 1)]
        public string Email { get; set; }

        [StringLength(30)]
        [Column(Order = 2)]
        public string FirstName { get; set; }

        [StringLength(30)]
        [Column(Order = 3)]
        public string LastName { get; set; }

        [Column(Order = 4)]
        public bool? IsFCUser { get; set; }

        [Column(Order = 5)]
        public DateTime LastLogIn { get; set; }
        [StringLength(255)]
        public string Note { get; set; }

        [NotMapped]
        public string FullName { get { return String.Format("{0} {1})", FirstName, LastName); } }
    }
}
