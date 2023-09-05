using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MCE_Electronic_Orders.Models
{
    [ModelMetadataType(typeof(MarketBranchDtlMetaData))]
    public partial class MarketBranchDtl
    {

    }
    public class MarketBranchDtlMetaData
    {
        [Display(Name = "رقم الطلب")]
        [Key]
        public Int64? OrderNo { get; set; }
        [Display(Name = "رقم المادة")]
        [RegularExpression(@"^\$?\d+(\.(\d{1}))?$")]
        public Int64? ItemNo { get; set; }
        [Display(Name = "اسم المادة")]
        public string ItemName { get; set; }
        [Display(Name = "الكمية")]
        public Int64? ItemQty { get; set; }
        [Display(Name = "حالة المادة")]
        public Int64? ItemStatus { get; set; }
        [Display(Name = "تاريخ الدخول")]
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "اسم المستخدم")]
        public string CreatedUser { get; set; }
        [Display(Name = "اسم المعدل")]
        public string ModifiedUser { get; set; }
        [Display(Name = "تاريخ التعديل")]
        public DateTime? ModifiedDate { get; set; }
        [Display(Name = "سعر المادة")]
        public decimal? ItemPrice { get; set; }
        [Display(Name = "المجموع")]
        public decimal? ItemTotalPrice { get; set; }
        [Display(Name = "رقم السوق")]
        public Int64 BranchIdNo { get; set; }

        public virtual MarketBranchHd OrderNoNavigation { get; set; }
    }
}
