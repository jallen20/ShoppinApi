using System;
using System.Collections.Generic;

namespace ShoppinAPICore.Models
{
    public partial class User
    {
        public User()
        {
            Address = new HashSet<Address>();
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string UserTypeId { get; set; }
        public virtual ICollection<Address> Address { get; set; }
    }
}
