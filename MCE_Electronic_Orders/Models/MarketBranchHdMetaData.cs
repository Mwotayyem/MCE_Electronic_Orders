using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MCE_Electronic_Orders.Models
{
    [ModelMetadataType(typeof(MarketBranchHdMetaData))]
    public partial class MarketBranchHd
    {

    }
    public class MarketBranchHdMetaData
    {
        [Display(Name = "رقم الطلب")]
        public Int64 OrderNo { get; set; }
        [Display(Name = "رقم الشركة")]
        public Int64? CompNo { get; set; }
        [Display(Name = "اسم الشركة")]
        public string CompName { get; set; }
        [Display(Name = "رقم الاتفاقية")]
        public Int64? AggNo { get; set; }
        [Display(Name = "تاريخ الطلب")]
        public DateTime? RequsetDate { get; set; }
        [Display(Name = "تسلسل الفاتورة")]
        public Int64? ReceiptSer { get; set; }
        [Display(Name = "حالة المادة")]
        public Int64? Status { get; set; }
        [Display(Name = "اسم المستخدم")]
        public string CreatedUser { get; set; }
        [Display(Name = "تاريخ الدخول")]
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "تاريخ التعديل")]
        public DateTime? ModifiedDate { get; set; }
        [Display(Name = "اسم المعدل")]
        public string ModifiedUser { get; set; }
        [Display(Name = "حالة الطلب")]
        public Int64 Flag { get; set; }

        public virtual ICollection<MarketBranchDtl> MarketBranchDtls { get; set; }
    }
}
