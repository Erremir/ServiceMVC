using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceMVC.Data;
using ServiceMVC.Models;

namespace ServiceMVC.Controllers
{
    public class RankingDiagsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RankingDiagsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RankingDiags
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RankingDiags.Include(r => r.ProbxDiag);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RankingDiags/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rankingDiag = await _context.RankingDiags
                .Include(r => r.ProbxDiag)
                .FirstOrDefaultAsync(m => m.RankingDiagID == id);
            if (rankingDiag == null)
            {
                return NotFound();
            }

            return View(rankingDiag);
        }

        // GET: RankingDiags/Create
        public IActionResult Create()
        {
            ViewData["ProbxDiagID"] = new SelectList(_context.ProbxDiags, "ProbxDiagID", "ProbxDiagID");
            return View();
        }

        // POST: RankingDiags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RankingDiagID,ProbxDiagID,Total")] RankingDiag rankingDiag)
        {
            if (ModelState.IsValid)
            {
                rankingDiag.RankingDiagID = Guid.NewGuid();
                _context.Add(rankingDiag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProbxDiagID"] = new SelectList(_context.ProbxDiags, "ProbxDiagID", "ProbxDiagID", rankingDiag.ProbxDiagID);
            return View(rankingDiag);
        }

        // GET: RankingDiags/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rankingDiag = await _context.RankingDiags.FindAsync(id);
            if (rankingDiag == null)
            {
                return NotFound();
            }
            ViewData["ProbxDiagID"] = new SelectList(_context.ProbxDiags, "ProbxDiagID", "ProbxDiagID", rankingDiag.ProbxDiagID);
            return View(rankingDiag);
        }

        // POST: RankingDiags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RankingDiagID,ProbxDiagID,Total")] RankingDiag rankingDiag)
        {
            if (id != rankingDiag.RankingDiagID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rankingDiag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RankingDiagExists(rankingDiag.RankingDiagID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProbxDiagID"] = new SelectList(_context.ProbxDiags, "ProbxDiagID", "ProbxDiagID", rankingDiag.ProbxDiagID);
            return View(rankingDiag);
        }

        // GET: RankingDiags/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rankingDiag = await _context.RankingDiags
                .Include(r => r.ProbxDiag)
                .FirstOrDefaultAsync(m => m.RankingDiagID == id);
            if (rankingDiag == null)
            {
                return NotFound();
            }

            return View(rankingDiag);
        }

        // POST: RankingDiags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var rankingDiag = await _context.RankingDiags.FindAsync(id);
            _context.RankingDiags.Remove(rankingDiag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RankingDiagExists(Guid id)
        {
            return _context.RankingDiags.Any(e => e.RankingDiagID == id);
        }
    }
}
