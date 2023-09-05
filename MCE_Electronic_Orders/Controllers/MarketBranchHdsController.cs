using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MCE_Electronic_Orders.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;

namespace MCE_Electronic_Orders.Controllers
{
    [Authorize(Roles = "store,admin")]
    public class MarketBranchHdsController : Controller
    {
        long Max;
        private readonly ModelContext _context;
        private Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager;
        public AppIdentityDbContext _contextUsers = new AppIdentityDbContext();


        public MarketBranchHdsController(ModelContext context, AppIdentityDbContext iden)
        {
            _context = context;
            _contextUsers = iden;
        }

        // GET: MarketBranchHds
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.MarketBranchHds.ToListAsync());
        //}

        //// GET: MarketBranchHds/Details/5
        //public async Task<IActionResult> Details(decimal? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var marketBranchHd = await _context.MarketBranchHds
        //        .FirstOrDefaultAsync(m => m.OrderNo == id);
        //    if (marketBranchHd == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(marketBranchHd);
        //}

        // GET: MarketBranchHds/Create
        public IActionResult Create(Mixed_Header_Details MHD)
        {

            //var _Companies2 = _context.CommdivViews.Select(o => new { o.CompName, o.AgrmntCompCode }).OrderBy(v => v.CompName).Distinct();

            //ViewBag.CompNo = new SelectList(_Companies2, "AgrmntCompCode", "CompName");
            ////listi<string> mixed = (from H in _context.MarketBranchHds
            ////                      join D in _context.MarketBranchDtls
            ////                      on H.OrderNo equals D.OrderNo
            ////                      select H.ToString()).ToList();
            //var model = new Mixed_Header_Details()
            //{
            //    Header_Collection = _context.MarketBranchHds.ToList(),
            //    Details_Collection = _context.MarketBranchDtls.ToList()
            //    //Mixed_Header_Collection = (IEnumerable<Mixed_Header_Details>)mixed.ToList()
            //};

            return View(Get_Header_Details(MHD));

        }

        // POST: MarketBranchHds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MarketBranchHd marketBranchHd, Mixed_Header_Details MHD)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    long _Flag;
                    long orderNo;
                    try
                    {
                        orderNo = long.Parse(_context.MarketBranchHds.Where(m => m.Flag == 0).Max(o => o.OrderNo).ToString());
                        if (orderNo == 0)
                        {
                            orderNo = long.Parse(_context.MarketBranchHds.Max(o => o.OrderNo + 1).ToString());

                        }
                        var cc = _context.MarketBranchHds.Select(o => new { o.Flag, o.OrderNo, o.BranchNo }).Where(m => m.BranchNo == GetUserBranch() && m.OrderNo == orderNo).SingleOrDefault();
                        if (cc is null)
                        {
                            _Flag = -1;
                        }
                        else
                        {
                            _Flag = long.Parse(cc.Flag.ToString());

                        }

                    }
                    catch (Exception)
                    {

                        _Flag = -1;
                    }




                    if (int.Parse(_Flag.ToString()) == -1)
                    {


                        marketBranchHd.CreatedDate = DateTime.Now;
                        marketBranchHd.RequsetDate = DateTime.Now;
                        marketBranchHd.CreatedUser = HttpContext.User.Identity.Name;
                        marketBranchHd.BranchNo = GetUserBranch();
                        MHD.CreatedUser2 = HttpContext.User.Identity.Name;
                        MHD.CreatedDate2 = DateTime.Now;
                        // إضافة بيانات إلى الجدول MarketBranchHd
                        _context.MarketBranchHds.Add(marketBranchHd);


                        // إضافة بيانات إلى الجدول MarketBranchDtl
                        var marketBranchDtl = new MarketBranchDtl
                        {
                            OrderNo = marketBranchHd.OrderNo,
                            ItemNo = MHD.ItemNo,
                            ItemName = MHD.ItemName,
                            ItemQty = MHD.ItemQty,
                            ItemStatus = 1,
                            CreatedDate = MHD.CreatedDate2,
                            CreatedUser = MHD.CreatedUser2,
                            ModifiedUser = MHD.ModifiedUser2,
                            ModifiedDate = MHD.ModifiedDate2,
                            ItemPrice = MHD.ItemPrice,
                            ItemTotalPrice = MHD.ItemTotalPrice


                        };


                        _context.MarketBranchDtls.Add(marketBranchDtl);

                        await _context.SaveChangesAsync();


                        return View(Get_Header_Details(MHD));
                        //return RedirectToAction(nameof(Create));
                        //return RedirectToAction("Create", Get_Header_Details(MHD));
                    }
                    else
                    {
                        MHD.CreatedUser2 = HttpContext.User.Identity.Name;
                        MHD.CreatedDate2 = DateTime.Now;


                        MHD.CreatedDate2 = DateTime.Now;

                        long orderNo2 = long.Parse(_context.MarketBranchHds.Max(o => o.OrderNo).ToString());

                        //إضافة بيانات إلى الجدول MarketBranchDtl
                        var marketBranchDtl = new MarketBranchDtl
                        {
                            OrderNo = orderNo2,
                            ItemNo = MHD.ItemNo,
                            ItemName = MHD.ItemName,
                            ItemQty = MHD.ItemQty,
                            ItemStatus = 1,
                            CreatedDate = MHD.CreatedDate2,
                            CreatedUser = MHD.CreatedUser2,
                            ModifiedUser = MHD.ModifiedUser2,
                            ModifiedDate = MHD.ModifiedDate2,
                            ItemPrice = MHD.ItemPrice,
                            ItemTotalPrice = MHD.ItemTotalPrice

                        };

                        _context.MarketBranchDtls.Add(marketBranchDtl);

                        await _context.SaveChangesAsync();
                        return View(Get_Header_Details(MHD));
                        //return RedirectToAction(nameof(Create));
                        //return RedirectToAction("Create", Get_Header_Details(MHD));
                    }

                }
                else
                {
                    return BadRequest("Failed to insert!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET: MarketBranchHds/Delete/5
        public async Task<IActionResult> Delete(Int64? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marketBranchHd = await _context.MarketBranchHds
                .FirstOrDefaultAsync(m => m.OrderNo == id);
            if (marketBranchHd == null)
            {
                return NotFound();
            }

            return View(marketBranchHd);
        }

        // POST: MarketBranchHds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Int64 id)
        {
            try
            {


                var marketBranchHd = _context.MarketBranchHds.Find(id);
                var orderno = marketBranchHd.OrderNo;

                var marketBranchDTL = _context.MarketBranchDtls.Where(x => x.OrderNo == orderno).ToList();
                //var marketBranchDTL =  _context.MarketBranchDtls.Find(orderno);
                _context.MarketBranchDtls.RemoveRange(marketBranchDTL);
                _context.MarketBranchHds.Remove(marketBranchHd);
                _context.SaveChanges();
                return RedirectToAction(nameof(Create));
            }
            catch (Exception)
            {
                var marketBranchHd = _context.MarketBranchHds.Find(id);
                _context.MarketBranchHds.Remove(marketBranchHd);
                _context.SaveChanges();
                return RedirectToAction(nameof(Create));
            }
        }

        private bool MarketBranchHdExists(Int64 id)
        {
            return _context.MarketBranchHds.Any(e => e.OrderNo == id);
        }

        [HttpPost]
        public JsonResult GetMaxOrder()
        {
            try
            {

                Max = long.Parse(_context.MarketBranchHds.Max(o => o.OrderNo).ToString());
                if (Max.ToString().Length > 0)
                {
                    return new JsonResult(Max + 1);
                }
                else
                {
                    return Json(1);
                    //return Json(1, System.Web.Mvc.JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                return Json(1);
            }

        }

        [HttpPost]
        public JsonResult ReceiptSer()
        {
            try
            {

                long receipt_ser = long.Parse(_context.OrdarsSerials.Where(m => m.SerUnit.Contains(GetUserBranch().ToString())).Max(o => o.SerV2serial).ToString());
                if (receipt_ser.ToString().Length > 0)
                {
                    return new JsonResult(receipt_ser);
                }
                else
                {
                    return Json(1);
                }
            }
            catch (Exception ex)
            {

                return Json(1);
            }

        }

        [HttpPost]
        public JsonResult GetData(string Barcode,Int64 CompNo)
        {
            if (Barcode != null && Barcode.Length >= 13)
            {
                var _AggNo = _context.CommdivViews.Select(o => new { o.FullName, o.ItemCode, o.ItemSalesPrice, o.Barcode, o.AgrmntNo }).Where(m => m.Barcode.Contains(Barcode)).Max(d => d.AgrmntNo);
                var _Item = _context.CommdivViews.Select(o => new { o.FullName, o.ItemCode, o.ItemSalesPrice, o.Barcode, o.AgrmntNo,o.AgrmntCompCode }).Where(m => m.Barcode.Contains(Barcode) && m.AgrmntNo == _AggNo && m.AgrmntCompCode==CompNo);

                if (_Item.Count() > 0)
                {
                    return new JsonResult(_Item.ToList());
                }
                else
                {
                    return Json(null, System.Web.Mvc.JsonRequestBehavior.AllowGet);


                }
            }
            else
            {
                return Json(null, System.Web.Mvc.JsonRequestBehavior.AllowGet);

            }

        }


        public IActionResult Edit_Flag(Int64 order, Mixed_Header_Details MHD)
        {


            MarketBranchHd MHD2 = _context.MarketBranchHds.Where(f => f.OrderNo == order).FirstOrDefault();
            if (MHD2 == null)
            {
                throw new Exception("");
            }
            MHD2.Flag = 1;
            MHD2.ModifiedDate = DateTime.Now;
            MHD2.ModifiedUser = HttpContext.User.Identity.Name;
            //_context.Update(marketBranchHd);
            _context.SaveChanges();
            //---------------------------------------------
            return View("Create", Get_Header_Details(MHD));

        }
        [HttpPost]
        public JsonResult GetUserBranch1()
        {
            var userid1 = User.Identity.GetUserId();
            var username = _contextUsers.Users.Select(o => new { o.Id, o.BRANCHID, o.Email }).Where(m => m.Id == userid1).SingleOrDefault();
            long BRANCHID = long.Parse(username.BRANCHID.ToString());
            var MarketDetails = _context.OrdersMarkets.Select(o => new { o.MarketName, o.MarketId }).Where(x => x.MarketId == BRANCHID).ToList();
            string MarketName = MarketDetails.Select(o => o.MarketName).ToString();
            return new JsonResult(MarketDetails);

        }

        public JsonResult GetCurr_Status(int type, int code)
        {
            var _Status = _context.OrdersLookupTables.Select(o => new { o.Type, o.Code, o.Descertion }).Where(x => x.Code == code && x.Type == type).OrderBy(v => v.Type);

            return new JsonResult(_Status);

        }

        public Int64 GetUserBranch()
        {
            var userid1 = User.Identity.GetUserId();
            var username = _contextUsers.Users.Select(o => new { o.Id, o.BRANCHID, o.Email }).Where(m => m.Id == userid1).SingleOrDefault();
            ViewBag.Branch_id = username.BRANCHID.ToString();
            return (Int64.Parse(username.BRANCHID.ToString()));

        }
        public Mixed_Header_Details Get_Header_Details(Mixed_Header_Details MHD)
        {
            var _Companies2 = _context.CommdivViews.Select(o => new { o.CompName, o.AgrmntCompCode }).OrderBy(v => v.CompName).Distinct();
            ViewBag.CompNo = new SelectList(_Companies2, "AgrmntCompCode", "CompName");


            List<MarketBranchDtl> query = (from d in _context.MarketBranchDtls
                                           join h in _context.MarketBranchHds on d.OrderNo equals h.OrderNo
                                           where h.Flag == 0
                                           select d).ToList();

            var model = new Mixed_Header_Details()
            {
                Header_Collection = _context.MarketBranchHds.Where(c => c.Flag == 0).ToList(),
                Details_Collection = query.AsEnumerable()
            };
            return (model);

        }

        public JsonResult GetCurr_AggNo(long CompCode)
        {
            var _Companies_AggNo = long.Parse(_context.CommdivViews.Select(o => new { o.AgrmntNo, o.AgrmntCompCode, o.AgrmntEndDate }).Where(x => x.AgrmntCompCode == CompCode).Max(o => o.AgrmntNo).ToString());

            return new JsonResult(_Companies_AggNo);

        }
    }
}









//// GET: MarketBranchHds/Edit/5
//public async Task<IActionResult> Edit(decimal? id)
//{
//    if (id == null)
//    {
//        return NotFound();
//    }

//    var marketBranchHd = await _context.MarketBranchHds.FindAsync(id);
//    if (marketBranchHd == null)
//    {
//        return NotFound();
//    }
//    return View(marketBranchHd);
//}





//[HttpPost]
//[ValidateAntiForgeryToken]
//public async Task<IActionResult> Edit(decimal id, [Bind("OrderNo,CompNo,CompName,AggNo,RequsetDate,ReceiptSer,Status,CreatedUser,CreatedDate,ModifiedDate,ModifiedUser")] MarketBranchHd marketBranchHd)
//{

//    if (id != marketBranchHd.OrderNo)
//    {
//        return NotFound();
//    }

//    if (ModelState.IsValid)
//    {
//        try
//        {
//            _context.Update(marketBranchHd);
//            await _context.SaveChangesAsync();
//        }
//        catch (DbUpdateConcurrencyException)
//        {
//            if (!MarketBranchHdExists(marketBranchHd.OrderNo))
//            {
//                return NotFound();
//            }
//            else
//            {
//                throw;
//            }
//        }
//        return RedirectToAction(nameof(Index));
//    }
//    return View(marketBranchHd);
//}