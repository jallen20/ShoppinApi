using System;
using System.Collections.Generic;

namespace ShoppinAPICore.Models
{
    public partial class Inventory
    {
        public int InventoryId { get; set; }
        public int InventoryItemId { get; set; }
        public int StoreId { get; set; }
    }
}
