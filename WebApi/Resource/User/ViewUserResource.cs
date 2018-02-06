using System;

namespace WebApi.Resource.User
{
    public class ViewUserResource
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? IsFCUser { get; set; }
        public DateTime LastLogIn { get; set; }
        public string Note { get; set; }
        public bool Active { get; set; }
        public DateTime PasswordExpired { get; set; }
        public string FullName { get { return String.Format("{0} {1}", FirstName, LastName); } }
    }
}
