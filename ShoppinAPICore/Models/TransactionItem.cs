using System;
using System.Collections.Generic;

namespace ShoppinAPICore.Models
{
    public partial class TransactionItem
    {
        public int TransactionItemId { get; set; }
        public int TransactionId { get; set; }
        public int InventoryItemId { get; set; }
        public int Quantity { get; set; }
    }
}
