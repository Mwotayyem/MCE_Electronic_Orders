using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MCE_Electronic_Orders.Models
{
    public class Mixed
    {
        public Int64 OrderNo { get; set; }
        public Int64 ItemNo { get; set; }
        public string ItemName { get; set; }
        public Int64? ItemQty { get; set; }
        public Int64? ItemStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public string ModifiedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public decimal? ItemPrice { get; set; }
        public decimal? ItemTotalPrice { get; set; }
        public Int64 BranchIdNo { get; set; }






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


        public virtual MarketBranchHd OrderNoNavigation { get; set; }
        public IEnumerable<MarketBranchHd> MarketBranchHds { get; set; }
        public IEnumerable<MarketBranchDtl> MarketBranchDtls { get; set; }
        public IEnumerable<Mixed_Header_Details> MHD { get; set; }

        public IEnumerable<OrdersMarket> OrdersMarket { get; set; }
        public IEnumerable<OrdersLookupTable> OrdersLookupTable { get; set; }

    }
}
