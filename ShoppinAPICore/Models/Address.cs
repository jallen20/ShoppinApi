using System;
using System.Collections.Generic;

namespace ShoppinAPICore.Models
{
    public partial class Address
    {
        public int AddressId { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
