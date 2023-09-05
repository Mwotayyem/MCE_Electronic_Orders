using System;
using System.Collections.Generic;

#nullable disable

namespace MCE_Electronic_Orders.Models
{
    public partial class MarketBranchHd
    {
        public MarketBranchHd()
        {
            MarketBranchDtls = new HashSet<MarketBranchDtl>();
        }

        public Int64 OrderNo { get; set; }
        public Int64? CompNo { get; set; }
        public string CompName { get; set; }
        public Int64? AggNo { get; set; }
        public DateTime? RequsetDate { get; set; }
        public Int64? ReceiptSer { get; set; }
        public Int64? Status { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public Int64 Flag { get; set; }
        public Int64 BranchNo { get; set; }

        public virtual ICollection<MarketBranchDtl> MarketBranchDtls { get; set; }
    }
}
