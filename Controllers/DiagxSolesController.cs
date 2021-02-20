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
    public class DiagxSolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiagxSolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DiagxSoles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DiagxSoles.Include(d => d.Diagnostico).Include(d => d.Solucion);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DiagxSoles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagxSol = await _context.DiagxSoles
                .Include(d => d.Diagnostico)
                .Include(d => d.Solucion)
                .FirstOrDefaultAsync(m => m.DiagxSolID == id);
            if (diagxSol == null)
            {
                return NotFound();
            }

            return View(diagxSol);
        }

        // GET: DiagxSoles/Create
        public IActionResult Create()
        {
            ViewData["DiagnosticoID"] = new SelectList(_context.Diagnosticos, "DiagnosticoID", "DiagnosticoID");
            ViewData["SolucionID"] = new SelectList(_context.Soluciones, "SolucionID", "Descripcion");
            return View();
        }

        // POST: DiagxSoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiagxSolID,DiagnosticoID,SolucionID")] DiagxSol diagxSol)
        {
            if (ModelState.IsValid)
            {
                diagxSol.DiagxSolID = Guid.NewGuid();
                _context.Add(diagxSol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiagnosticoID"] = new SelectList(_context.Diagnosticos, "DiagnosticoID", "DiagnosticoID", diagxSol.DiagnosticoID);
            ViewData["SolucionID"] = new SelectList(_context.Soluciones, "SolucionID", "Descripcion", diagxSol.SolucionID);
            return View(diagxSol);
        }

        // GET: DiagxSoles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagxSol = await _context.DiagxSoles.FindAsync(id);
            if (diagxSol == null)
            {
                return NotFound();
            }
            ViewData["DiagnosticoID"] = new SelectList(_context.Diagnosticos, "DiagnosticoID", "DiagnosticoID", diagxSol.DiagnosticoID);
            ViewData["SolucionID"] = new SelectList(_context.Soluciones, "SolucionID", "Descripcion", diagxSol.SolucionID);
            return View(diagxSol);
        }

        // POST: DiagxSoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DiagxSolID,DiagnosticoID,SolucionID")] DiagxSol diagxSol)
        {
            if (id != diagxSol.DiagxSolID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diagxSol);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagxSolExists(diagxSol.DiagxSolID))
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
            ViewData["DiagnosticoID"] = new SelectList(_context.Diagnosticos, "DiagnosticoID", "DiagnosticoID", diagxSol.DiagnosticoID);
            ViewData["SolucionID"] = new SelectList(_context.Soluciones, "SolucionID", "Descripcion", diagxSol.SolucionID);
            return View(diagxSol);
        }

        // GET: DiagxSoles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagxSol = await _context.DiagxSoles
                .Include(d => d.Diagnostico)
                .Include(d => d.Solucion)
                .FirstOrDefaultAsync(m => m.DiagxSolID == id);
            if (diagxSol == null)
            {
                return NotFound();
            }

            return View(diagxSol);
        }

        // POST: DiagxSoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var diagxSol = await _context.DiagxSoles.FindAsync(id);
            _context.DiagxSoles.Remove(diagxSol);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiagxSolExists(Guid id)
        {
            return _context.DiagxSoles.Any(e => e.DiagxSolID == id);
        }
    }
}
