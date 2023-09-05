using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace MCE_Electronic_Orders.Models
{
    public partial class MarketBranchDtl
    {
        [Key]
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

        public virtual MarketBranchHd OrderNoNavigation { get; set; }
    }
}
