using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DomainLibrary.Member
{
    [Table("Users")]
    public class User
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
        public bool Active { get; set; }
        public DateTime PasswordExpired { get; set; }
        public DateTime AddedOn { get; set; }
        public int? LastUpdatedById { get; set; }
        public DateTime? LastUpdatedByOn { get; set; }

        [NotMapped]
        public string FullName { get { return String.Format("{0} {1}", FirstName, LastName); } }

        public ICollection<UserPassword> UserPasswords { get; set; }
        [NotMapped]
        public UserPassword LatestUserPassword { get { return (UserPasswords != null && UserPasswords.Count > 0) ? UserPasswords.Where(up => up.Active).FirstOrDefault() : null; } }

        public User()
        {
            UserPasswords = new Collection<UserPassword>();
        }
    }
}
