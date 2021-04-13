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
    public class ProbxDiagsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProbxDiagsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProbxDiags
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProbxDiags.Include(p => p.Diagnostico).Include(p => p.Problema);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProbxDiags/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var probxDiag = await _context.ProbxDiags
                .Include(p => p.Diagnostico)
                .Include(p => p.Problema)
                .FirstOrDefaultAsync(m => m.ProbxDiagID == id);
            if (probxDiag == null)
            {
                return NotFound();
            }

            return View(probxDiag);
        }

        // GET: ProbxDiags/Create
        public IActionResult Create()
        {
            ViewData["DiagnosticoID"] = new SelectList(_context.Diagnosticos, "DiagnosticoID", "DiagnosticoID");
            ViewData["ProblemaID"] = new SelectList(_context.Problemas, "ProblemaID", "ProblemaID");
            return View();
        }

        // POST: ProbxDiags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProbxDiagID,ProblemaID,DiagnosticoID")] ProbxDiag probxDiag)
        {
            if (ModelState.IsValid)
            {
                probxDiag.ProbxDiagID = Guid.NewGuid();
                _context.Add(probxDiag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiagnosticoID"] = new SelectList(_context.Diagnosticos, "DiagnosticoID", "DiagnosticoID", probxDiag.DiagnosticoID);
            ViewData["ProblemaID"] = new SelectList(_context.Problemas, "ProblemaID", "ProblemaID", probxDiag.ProblemaID);
            return View(probxDiag);
        }

        // GET: ProbxDiags/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var probxDiag = await _context.ProbxDiags.FindAsync(id);
            if (probxDiag == null)
            {
                return NotFound();
            }
            ViewData["DiagnosticoID"] = new SelectList(_context.Diagnosticos, "DiagnosticoID", "DiagnosticoID", probxDiag.DiagnosticoID);
            ViewData["ProblemaID"] = new SelectList(_context.Problemas, "ProblemaID", "ProblemaID", probxDiag.ProblemaID);
            return View(probxDiag);
        }

        // POST: ProbxDiags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProbxDiagID,ProblemaID,DiagnosticoID")] ProbxDiag probxDiag)
        {
            if (id != probxDiag.ProbxDiagID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(probxDiag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProbxDiagExists(probxDiag.ProbxDiagID))
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
            ViewData["DiagnosticoID"] = new SelectList(_context.Diagnosticos, "DiagnosticoID", "DiagnosticoID", probxDiag.DiagnosticoID);
            ViewData["ProblemaID"] = new SelectList(_context.Problemas, "ProblemaID", "ProblemaID", probxDiag.ProblemaID);
            return View(probxDiag);
        }

        // GET: ProbxDiags/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var probxDiag = await _context.ProbxDiags
                .Include(p => p.Diagnostico)
                .Include(p => p.Problema)
                .FirstOrDefaultAsync(m => m.ProbxDiagID == id);
            if (probxDiag == null)
            {
                return NotFound();
            }

            return View(probxDiag);
        }

        // POST: ProbxDiags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var probxDiag = await _context.ProbxDiags.FindAsync(id);
            _context.ProbxDiags.Remove(probxDiag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProbxDiagExists(Guid id)
        {
            return _context.ProbxDiags.Any(e => e.ProbxDiagID == id);
        }
    }
}
