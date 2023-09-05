using System;
using System.Collections.Generic;

#nullable disable

namespace MCE_Electronic_Orders.Models
{
    public partial class StoreView
    {
        public string MssStno { get; set; }
        public DateTime MssExdate { get; set; }
        public string MssStore { get; set; }
        public string MssLinkno { get; set; }
        public DateTime? MssStdate { get; set; }
        public string MssType { get; set; }
        public int? MssCqty { get; set; }
        public int? MssQty { get; set; }
        public string MssProd { get; set; }
        public string MssStat { get; set; }
        public string MssStop { get; set; }
        public string MssOrder { get; set; }
        public string MssTtyno { get; set; }
        public DateTime? MssEnterDate { get; set; }
        public string MssFlag { get; set; }
        public DateTime? Exdate { get; set; }
    }
}
