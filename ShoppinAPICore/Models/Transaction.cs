using System;
using System.Collections.Generic;

namespace ShoppinAPICore.Models
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public string BuyerEmail { get; set; }
        public string SellerEmail { get; set; }
        public int StoreId { get; set; }
        public string TransactionTypeId { get; set; }
        public string DeliveryTypeId { get; set; }
        public decimal TransactionTotal { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
