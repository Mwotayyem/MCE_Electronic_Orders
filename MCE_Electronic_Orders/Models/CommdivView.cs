using System;
using System.Collections.Generic;

#nullable disable

namespace MCE_Electronic_Orders.Models
{
    public partial class CommdivView
    {
        public string AgrmntNo { get; set; }
        public string AggSbstr { get; set; }
        public Int64 AgrmntCompCode { get; set; }
        public decimal? AgrmntTypeCode { get; set; }
        public DateTime? AgrmntStartDate { get; set; }
        public DateTime? AgrmntEndDate { get; set; }
        public string AgrmntReciveType { get; set; }
        public string AgrmntNote { get; set; }
        public decimal AggStatus { get; set; }
        public decimal? AgrmntTarget { get; set; }
        public decimal? AgrmntProfitRatio { get; set; }
        public decimal ItmesStatus { get; set; }
        public decimal? ItemPurcahsePrice { get; set; }
        public decimal? ItemSalesPrice { get; set; }
        public string CompName { get; set; }
        public decimal? CompStatus { get; set; }
        public DateTime? CompStartDate { get; set; }
        public string Barcode { get; set; }
        public string ItemCode { get; set; }
        public string FullName { get; set; }
    }
}
