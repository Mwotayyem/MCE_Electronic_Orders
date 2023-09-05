using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MCE_Electronic_Orders.Models;
using Microsoft.AspNetCore.Authorization;

namespace MCE_Electronic_Orders.Controllers
{
    [Authorize]
    public class MarketBranchDtlsController : Controller
    {
        private readonly ModelContext _context;

        public MarketBranchDtlsController(ModelContext context)
        {
            _context = context;
        }


        // GET: MarketBranchDtls/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marketBranchDtl = await _context.MarketBranchDtls
                .Include(m => m.OrderNoNavigation)
                .FirstOrDefaultAsync(m => m.BranchIdNo == id);
            if (marketBranchDtl == null)
            {
                return NotFound();
            }

            return View(marketBranchDtl);
        }

        // POST: MarketBranchDtls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(decimal id)
        {
            var marketBranchDTL = _context.MarketBranchDtls.Where(x => x.BranchIdNo == id).FirstOrDefault();
            _context.MarketBranchDtls.Remove(marketBranchDTL);
            _context.SaveChanges();
            return RedirectToAction("create", "MarketBranchHds");
        }

        //public async Task<IActionResult> DeleteConfirmed(decimal id)
        //{
        //    var marketBranchDtl = await _context.MarketBranchDtls.FindAsync(id);
        //    _context.MarketBranchDtls.Remove(marketBranchDtl);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}


        //// GET: MarketBranchDtls
        //public async Task<IActionResult> Index()
        //{
        //    var modelContext = _context.MarketBranchDtls.Include(m => m.OrderNoNavigation);
        //    return View(await modelContext.ToListAsync());
        //}

        //// GET: MarketBranchDtls/Details/5
        //public async Task<IActionResult> Details(decimal? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var marketBranchDtl = await _context.MarketBranchDtls
        //        .Include(m => m.OrderNoNavigation)
        //        .FirstOrDefaultAsync(m => m.OrderNo == id);
        //    if (marketBranchDtl == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(marketBranchDtl);
        //}

        //// GET: MarketBranchDtls/Create
        //public IActionResult Create()
        //{
        //    ViewData["OrderNo"] = new SelectList(_context.MarketBranchHds, "OrderNo", "OrderNo");
        //    return View();
        //}

        //// POST: MarketBranchDtls/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("OrderNo,ItemNo,ItemName,ItemQty,ItemStatus,CreatedDate,CreatedUser,ModifiedUser,ModifiedDate,ItemPrice,ItemTotalPrice,BranchIdNo")] MarketBranchDtl marketBranchDtl)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(marketBranchDtl);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["OrderNo"] = new SelectList(_context.MarketBranchHds, "OrderNo", "OrderNo", marketBranchDtl.OrderNo);
        //    return View(marketBranchDtl);
        //}

        //// GET: MarketBranchDtls/Edit/5
        //public async Task<IActionResult> Edit(decimal? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var marketBranchDtl = await _context.MarketBranchDtls.FindAsync(id);
        //    if (marketBranchDtl == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["OrderNo"] = new SelectList(_context.MarketBranchHds, "OrderNo", "OrderNo", marketBranchDtl.OrderNo);
        //    return View(marketBranchDtl);
        //}

        //// POST: MarketBranchDtls/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(decimal id, [Bind("OrderNo,ItemNo,ItemName,ItemQty,ItemStatus,CreatedDate,CreatedUser,ModifiedUser,ModifiedDate,ItemPrice,ItemTotalPrice,BranchIdNo")] MarketBranchDtl marketBranchDtl)
        //{
        //    if (id != marketBranchDtl.OrderNo)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(marketBranchDtl);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!MarketBranchDtlExists(marketBranchDtl.OrderNo))
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
        //    ViewData["OrderNo"] = new SelectList(_context.MarketBranchHds, "OrderNo", "OrderNo", marketBranchDtl.OrderNo);
        //    return View(marketBranchDtl);
        //}



        //private bool MarketBranchDtlExists(decimal id)
        //{
        //    return _context.MarketBranchDtls.Any(e => e.OrderNo == id);
        //}
    }
}
