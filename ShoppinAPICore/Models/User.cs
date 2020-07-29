using System;
using System.Collections.Generic;

namespace ShoppinAPICore.Models
{
    public partial class User
    {

        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string UserTypeId { get; set; }

        public string AddressId { get; set; }
    }
}
