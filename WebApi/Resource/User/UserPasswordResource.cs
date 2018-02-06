using System;

namespace WebApi.Resource.User
{
    public class UserPasswordResource
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Password { get; set; }
        public DateTime PasswordCreated { get; set; }
        public bool Active { get; set; }
    }
}
