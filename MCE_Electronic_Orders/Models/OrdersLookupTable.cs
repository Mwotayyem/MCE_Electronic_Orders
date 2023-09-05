using System;
using System.Collections.Generic;

#nullable disable

namespace MCE_Electronic_Orders.Models
{
    public partial class OrdersLookupTable
    {
        public decimal? Type { get; set; }
        public decimal? Code { get; set; }
        public string Descertion { get; set; }
    }
}
