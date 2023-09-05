using System;
using System.Collections.Generic;

#nullable disable

namespace MCE_Electronic_Orders.Models
{
    public partial class OrdersMarket
    {
        public decimal MarketId { get; set; }
        public string MarketName { get; set; }
        public string IpAddress { get; set; }
        public decimal? RegionId { get; set; }
        public string RegionName { get; set; }
        public decimal? StoreCode { get; set; }
        public decimal? ActiveFlag { get; set; }
        public string DbLink { get; set; }
        public decimal? IsInvest { get; set; }
        public string NielsenCode { get; set; }
        public string TnsName { get; set; }
    }
}
