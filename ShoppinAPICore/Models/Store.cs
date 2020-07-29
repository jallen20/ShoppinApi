using System;
using System.Collections.Generic;

namespace ShoppinAPICore.Models
{
    public partial class Store
    {
        public int StoreId { get; set; }
        public string OwnerEmail { get; set; }
        public string AddressId { get; set; }
        public string Name { get; set; }
    }
}
