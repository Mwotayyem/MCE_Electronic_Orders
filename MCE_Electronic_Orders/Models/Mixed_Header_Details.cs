using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MCE_Electronic_Orders.Models
{
    public class Mixed_Header_Details
    {
      
            public Mixed_Header_Details()
            {
                MarketBranchDtls = new HashSet<MarketBranchDtl>();
            }

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
            [Display(Name = "حالة الطلب")]
            public Int64? Status { get; set; }
            [Display(Name = "اسم المستخدم")]
            public string CreatedUser { get; set; }
            [Display(Name = "تاريخ الدخول")]
            public DateTime? CreatedDate { get; set; }
            [Display(Name = "تاريخ التعديل")]
            public DateTime? ModifiedDate { get; set; }
            [Display(Name = "اسم المعدل")]
            public string ModifiedUser { get; set; }
          
        public virtual ICollection<MarketBranchDtl> MarketBranchDtls { get; set; }




             [Display(Name = "رقم الطلب")]
            public Int64? OrderNo2 { get; set; }
            [Display(Name = "رقم المادة")]

        [RegularExpression(@"^\$?\d+(\.(\d{1}))?$")]
        public Int64 ItemNo { get; set; }
            [Display(Name = "اسم المادة")]
            public string ItemName { get; set; }
            [Display(Name = "الكمية")]
            [Required]
            public Int64? ItemQty { get; set; }
            [Display(Name = "حالة المادة")]
            public Int64? ItemStatus { get; set; }
            [Display(Name = "تاريخ الدخول")]
            public DateTime? CreatedDate2 { get; set; }
            [Display(Name = "اسم المستخدم")]
            public string CreatedUser2 { get; set; }
            [Display(Name = "اسم المعدل")]
            public string ModifiedUser2 { get; set; }
            [Display(Name = "تاريخ التعديل")]
            public DateTime? ModifiedDate2 { get; set; }
            [Display(Name = "سعر المادة")]
            public decimal? ItemPrice { get; set; }
            [Display(Name = "المجموع")]
            public decimal? ItemTotalPrice { get; set; }
            [Display(Name = "رقم السوق")]
            public Int64 BranchIdNo { get; set; }
             

        public virtual MarketBranchHd OrderNoNavigation { get; set; }







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







        public IEnumerable<MarketBranchHd> Header_Collection { get; set; }
        public IEnumerable<MarketBranchDtl> Details_Collection { get; set; }


        //public IEnumerable<Mixed_Header_Details> Mixed_Header_Collection { get; set; }


       
    }
}
