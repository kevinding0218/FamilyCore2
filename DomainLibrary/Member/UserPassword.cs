using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLibrary.Member
{
    [Table("UserPassword")]
    public class UserPassword
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime PasswordCreated { get; set; }
        public bool Active { get; set; }

        public User CurrentUser { get; set; }
    }
}
