using System;
using System.Collections.Generic;

namespace ShoppinAPICore.Models
{
    public partial class UserType
    {
        public UserType()
        {
            User = new HashSet<User>();
        }

        public string UserTypeId { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
