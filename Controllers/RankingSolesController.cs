using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceMVC.Data;
using ServiceMVC.Models;

namespace ServiceMVC.Controllers
{
    [Authorize]
    public class RankingSolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RankingSolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RankingSoles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RankingSoles.Include(r => r.DiagxSol);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RankingSoles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rankingSol = await _context.RankingSoles
                .Include(r => r.DiagxSol)
                .FirstOrDefaultAsync(m => m.RankingSolID == id);
            if (rankingSol == null)
            {
                return NotFound();
            }

            return View(rankingSol);
        }

        // GET: RankingSoles/Create
        public IActionResult Create()
        {
            ViewData["DiagxSolID"] = new SelectList(_context.DiagxSoles, "DiagxSolID", "DiagxSolID");
            return View();
        }

        // POST: RankingSoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RankingSolID,DiagxSolID,Total")] RankingSol rankingSol)
        {
            if (ModelState.IsValid)
            {
                rankingSol.RankingSolID = Guid.NewGuid();
                _context.Add(rankingSol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiagxSolID"] = new SelectList(_context.DiagxSoles, "DiagxSolID", "DiagxSolID", rankingSol.DiagxSolID);
            return View(rankingSol);
        }

        // GET: RankingSoles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rankingSol = await _context.RankingSoles.FindAsync(id);
            if (rankingSol == null)
            {
                return NotFound();
            }
            ViewData["DiagxSolID"] = new SelectList(_context.DiagxSoles, "DiagxSolID", "DiagxSolID", rankingSol.DiagxSolID);
            return View(rankingSol);
        }

        // POST: RankingSoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RankingSolID,DiagxSolID,Total")] RankingSol rankingSol)
        {
            if (id != rankingSol.RankingSolID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rankingSol);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RankingSolExists(rankingSol.RankingSolID))
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
            ViewData["DiagxSolID"] = new SelectList(_context.DiagxSoles, "DiagxSolID", "DiagxSolID", rankingSol.DiagxSolID);
            return View(rankingSol);
        }

        // GET: RankingSoles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rankingSol = await _context.RankingSoles
                .Include(r => r.DiagxSol)
                .FirstOrDefaultAsync(m => m.RankingSolID == id);
            if (rankingSol == null)
            {
                return NotFound();
            }

            return View(rankingSol);
        }

        // POST: RankingSoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var rankingSol = await _context.RankingSoles.FindAsync(id);
            _context.RankingSoles.Remove(rankingSol);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RankingSolExists(Guid id)
        {
            return _context.RankingSoles.Any(e => e.RankingSolID == id);
        }
    }
}
