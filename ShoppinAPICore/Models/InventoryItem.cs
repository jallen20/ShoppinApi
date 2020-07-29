using System;
using System.Collections.Generic;

namespace ShoppinAPICore.Models
{
    public partial class InventoryItem
    {

        public int InventoryItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string InventoryItemStatus { get; set; }

    }
}
