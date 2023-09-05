using MCE_Electronic_Orders.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MCE_Electronic_Orders.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ModelContext _context;
        public AppIdentityDbContext _contextUsers = new AppIdentityDbContext();


        public OrdersController(ModelContext context, AppIdentityDbContext iden)
        {
            _context = context;
            _contextUsers = iden;
        }

        [Authorize(Roles = "comp,admin")]
        public ActionResult Index()
        {
           
            int pp, ss;
            try
            {
                var xx = Request.QueryString.Value;
                // Get a substring between two strings
                int firstStringPosition = xx.ToString().IndexOf("p=");
                int secondStringPosition = xx.ToString().IndexOf("s=");
                int thirdStringPosition = xx.ToString().IndexOf("&");
                string p = xx.ToString().Substring(firstStringPosition + 2, thirdStringPosition - 3);
                string s = xx.ToString().Substring(secondStringPosition + 2);

                pp = int.Parse(p);
                ss = int.Parse(s);
                GetUserBranch();
            }
            catch (Exception)
            {

                pp = 1;
                ss = 10;

                GetUserBranch();

            }
            return View(Indexpages(pp, ss));
        }

        [Authorize(Roles = "store,admin")]
        public ActionResult getfinshedorders()
        {

            int pp, ss;
            try
            {
                var xx = Request.QueryString.Value;
                // Get a substring between two strings
                int firstStringPosition = xx.ToString().IndexOf("p=");
                int secondStringPosition = xx.ToString().IndexOf("s=");
                int thirdStringPosition = xx.ToString().IndexOf("&");
                string p = xx.ToString().Substring(firstStringPosition + 2, thirdStringPosition - 3);
                string s = xx.ToString().Substring(secondStringPosition + 2);

                pp = int.Parse(p);
                ss = int.Parse(s);
                GetUserBranch();
            }
            catch (Exception)
            {

                pp = 1;
                ss = 10;

                GetUserBranch();

            }
            return View(Indexpages(pp, ss));
        }

        [HttpPost]
        public IActionResult getfinshedorders(decimal OrderNo)
        {
            var order = _context.MarketBranchHds.SingleOrDefault(o => o.OrderNo == OrderNo);
            if (order != null)
            {
                order.Status = 3;
                order.ModifiedDate = DateTime.Now;
                var userid1 = User.Identity.GetUserId();
                var username = _contextUsers.Users.Select(o => new { o.Id, o.BRANCHID, o.Email, o.UserName }).Where(m => m.Id == userid1).SingleOrDefault();
                order.ModifiedUser = username.UserName.ToString();
                _context.SaveChanges();
                return Json(new { success = true, message = "تم استلام الطلب." });
            }
            else
            {
                return Json(new { success = false, message = "لم يتم العثور على الطلب." });
            }
        }


        public JsonResult GetCurr_Status(int type, int code)
        {
            var _Status = _context.OrdersLookupTables.Select(o => new { o.Type, o.Code, o.Descertion }).Where(x => x.Code == code && x.Type == type).OrderBy(v => v.Type);

            return new JsonResult(_Status);

        }


        public Mixed Indexpages(int p = 1, int s = 10)
        {
   //         var x =
   //from d in _context.MarketBranchHds
   //join m in _context.OrdersMarkets on d.BranchNo equals m.MarketId
   //select d.AggNo + d.BranchNo + d.CompName + d.CompNo + d.CreatedDate + d.CreatedUser +
   //            d.Flag + d.ModifiedDate + d.ModifiedUser + d.OrderNo + d.ReceiptSer + d.RequsetDate + d.Status + m.MarketId + m.MarketName;
          
            var tables = new Mixed
           {
               MarketBranchDtls = _context.MarketBranchDtls.ToList(),

                MarketBranchHds = _context.MarketBranchHds.OrderBy(x => x.CreatedDate).Skip((p - 1) * s).Take(s),

                OrdersMarket = _context.OrdersMarkets.ToList(),

                OrdersLookupTable= _context.OrdersLookupTables.ToList()

            };

            ViewBag.TotalRecords = _context.MarketBranchHds.Count();
            ViewBag.PageNo = p;
            ViewBag.PageSize = s;


            return tables;
        }

        public IActionResult Index2()
        {
            // اعرض الصفحة الخاصة بالتأكيد من قبل المستخدم
            return View();
        }


        [HttpPost]
        public IActionResult Index(MarketBranchHd id)
        {
            MarketBranchHd u = (from c in this._context.MarketBranchHds
                                where (c.OrderNo == _context.MarketBranchHds.SingleOrDefault().OrderNo && c.Flag == 1)
                                select c).FirstOrDefault();
            GetUserBranch();



            //MarketBranchHd u = _context.MarketBranchHds.Where(c => c.OrderNo == _context.MarketBranchHds.SingleOrDefault().OrderNo && c.Flag == 1).FirstOrDefault();


            if (u != null)
            {
                u.Status = 1;

                this._context.SaveChanges();
                ViewBag.Message = "Customer record updated.";
            }
            else
            {
                ViewBag.Message = "Customer not found.";
            }

            return View();
        }

        public IActionResult GetDetails(decimal orderNo)
        {
            var details = _context.MarketBranchDtls
                .Where(d => d.OrderNo == orderNo)
                .ToList();

            return Json(details);
        }

        public IActionResult EditQuantity(decimal itemNo, int newQuantity, decimal OrderNo)
        {
            //var Ordern = _context.MarketBranchDtls.Select(s => s.OrderNo).Where(x => x.Equals(itemNo)).ToString().SingleOrDefault();
            var item = _context.MarketBranchDtls.Where(d => d.ItemNo == itemNo
            && d.OrderNo == OrderNo && d.ItemStatus == 1).SingleOrDefault();

            if (item != null)
            {
                // إجراء التحقق من عدم تجاوز الكمية المتاحة هنا
                if (newQuantity <= item.ItemQty && newQuantity != 0) // ضع هنا الشرط المناسب
                {
                    item.ItemQty = newQuantity;
                    _context.SaveChanges();
                    return Json(new { success = true, message = "تم تحديث الكمية بنجاح." });
                }
                else
                {
                    return Json(new { success = false, message = "الكمية المطلوبة تتجاوز الكمية المتاحة." });
                }
            }
            else
            {
                return Json(new { success = false, message = "العنصر غير موجود." });
            }
        }

        [HttpPost]
        public IActionResult FinishOrder(decimal OrderNo)
        {
            var order = _context.MarketBranchHds.SingleOrDefault(o => o.OrderNo == OrderNo);
            if (order != null)
            {
                order.Status = 2;
                order.ModifiedDate = DateTime.Now;
                var userid1 = User.Identity.GetUserId();
                var username = _contextUsers.Users.Select(o => new { o.Id, o.BRANCHID, o.Email, o.UserName }).Where(m => m.Id == userid1).SingleOrDefault();
                order.ModifiedUser = username.UserName.ToString();
                _context.SaveChanges();
                return Json(new { success = true, message = "تم إنهاء الطلب بنجاح." });
            }
            else
            {
                return Json(new { success = false, message = "لم يتم العثور على الطلب." });
            }
        }

        


        public Int64 GetUserBranch()
        {
            var userid1 = User.Identity.GetUserId();
            var username = _contextUsers.Users.Select(o => new { o.Id, o.BRANCHID, o.Email }).Where(m => m.Id == userid1).SingleOrDefault();
            ViewBag.Branch_id = username.BRANCHID.ToString();
            return (Int64.Parse(username.BRANCHID.ToString()));

        }

        public IActionResult Delete(Int64 itemId, Int64 orderno, Mixed_Header_Details MHD)
        {
            MarketBranchDtl DTL2 = _context.MarketBranchDtls.Where(f => f.ItemNo == itemId && f.OrderNo == orderno).FirstOrDefault();
            if (DTL2 == null)
            {
                throw new Exception("");
            }
            DTL2.ItemStatus = 3;
            DTL2.ModifiedDate = DateTime.Now;
            DTL2.ModifiedUser = HttpContext.User.Identity.Name;
            _context.SaveChanges();


            int pp, ss;
            try
            {
                var xx = Request.QueryString.Value;
                // Get a substring between two strings
                int firstStringPosition = xx.ToString().IndexOf("p=");
                int secondStringPosition = xx.ToString().IndexOf("s=");
                int thirdStringPosition = xx.ToString().IndexOf("&");
                string p = xx.ToString().Substring(firstStringPosition + 2, thirdStringPosition - 3);
                string s = xx.ToString().Substring(secondStringPosition + 2);

                pp = int.Parse(p);
                ss = int.Parse(s);
                GetUserBranch();
            }
            catch (Exception)
            {

                pp = 1;
                ss = 10;

                GetUserBranch();

            }
            return View(Indexpages(pp, ss));

        }

        public IActionResult UnDelete(Int64 itemId, Int64 orderno, Mixed_Header_Details MHD)
        {
            MarketBranchDtl DTL2 = _context.MarketBranchDtls.Where(f => f.ItemNo == itemId && f.OrderNo == orderno).FirstOrDefault();
            if (DTL2 == null)
            {
                throw new Exception("");
            }
            DTL2.ItemStatus = 1;
            DTL2.ModifiedDate = DateTime.Now;
            DTL2.ModifiedUser = HttpContext.User.Identity.Name;
            _context.SaveChanges();
            GetUserBranch();

            int pp, ss;
            try
            {
                var xx = Request.QueryString.Value;
                // Get a substring between two strings
                int firstStringPosition = xx.ToString().IndexOf("p=");
                int secondStringPosition = xx.ToString().IndexOf("s=");
                int thirdStringPosition = xx.ToString().IndexOf("&");
                string p = xx.ToString().Substring(firstStringPosition + 2, thirdStringPosition - 3);
                string s = xx.ToString().Substring(secondStringPosition + 2);

                pp = int.Parse(p);
                ss = int.Parse(s);
                GetUserBranch();
            }
            catch (Exception)
            {

                pp = 1;
                ss = 10;

                GetUserBranch();

            }
            return View(Indexpages(pp, ss));

        }

        [HttpPost]
        public JsonResult GetUserBranch1()
        {
            var userid1 = User.Identity.GetUserId();
            var username = _contextUsers.Users.Select(o => new { o.Id, o.BRANCHID, o.Email }).Where(m => m.Id == userid1).SingleOrDefault();
            long BRANCHID = long.Parse(username.BRANCHID.ToString());
            return new JsonResult(BRANCHID);

        }


    }

}










