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
    public class ProbDiagSolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProbDiagSolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProbDiagSoles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProbDiagSoles.Include(p => p.Diagnostico).Include(p => p.Problema).Include(p => p.Servicio).Include(p => p.Solucion);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProbDiagSoles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var probDiagSol = await _context.ProbDiagSoles
                .Include(p => p.Diagnostico)
                .Include(p => p.Problema)
                .Include(p => p.Servicio)
                .Include(p => p.Solucion)
                .FirstOrDefaultAsync(m => m.ProbDiagSolID == id);
            if (probDiagSol == null)
            {
                return NotFound();
            }

            return View(probDiagSol);
        }

        // GET: ProbDiagSoles/Create
        public IActionResult Create()
        {
            ViewData["DiagnosticoID"] = new SelectList(_context.Diagnosticos, "DiagnosticoID", "DiagnosticoID");
            ViewData["ProblemaID"] = new SelectList(_context.Problemas, "ProblemaID", "ProblemaID");
            ViewData["ServicioID"] = new SelectList(_context.Servicios, "ServicioID", "ServicioID");
            ViewData["SolucionID"] = new SelectList(_context.Soluciones, "SolucionID", "Descripcion");
            return View();
        }

        // POST: ProbDiagSoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProbDiagSolID,ServicioID,ProblemaID,DiagnosticoID,SolucionID")] ProbDiagSol probDiagSol)
        {
            if (ModelState.IsValid)
            {
                probDiagSol.ProbDiagSolID = Guid.NewGuid();
                _context.Add(probDiagSol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiagnosticoID"] = new SelectList(_context.Diagnosticos, "DiagnosticoID", "DiagnosticoID", probDiagSol.DiagnosticoID);
            ViewData["ProblemaID"] = new SelectList(_context.Problemas, "ProblemaID", "ProblemaID", probDiagSol.ProblemaID);
            ViewData["ServicioID"] = new SelectList(_context.Servicios, "ServicioID", "ServicioID", probDiagSol.ServicioID);
            ViewData["SolucionID"] = new SelectList(_context.Soluciones, "SolucionID", "Descripcion", probDiagSol.SolucionID);
            return View(probDiagSol);
        }

        // GET: ProbDiagSoles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var probDiagSol = await _context.ProbDiagSoles.FindAsync(id);
            if (probDiagSol == null)
            {
                return NotFound();
            }
            ViewData["DiagnosticoID"] = new SelectList(_context.Diagnosticos, "DiagnosticoID", "DiagnosticoID", probDiagSol.DiagnosticoID);
            ViewData["ProblemaID"] = new SelectList(_context.Problemas, "ProblemaID", "ProblemaID", probDiagSol.ProblemaID);
            ViewData["ServicioID"] = new SelectList(_context.Servicios, "ServicioID", "ServicioID", probDiagSol.ServicioID);
            ViewData["SolucionID"] = new SelectList(_context.Soluciones, "SolucionID", "Descripcion", probDiagSol.SolucionID);
            return View(probDiagSol);
        }

        // POST: ProbDiagSoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProbDiagSolID,ServicioID,ProblemaID,DiagnosticoID,SolucionID")] ProbDiagSol probDiagSol)
        {
            if (id != probDiagSol.ProbDiagSolID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(probDiagSol);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProbDiagSolExists(probDiagSol.ProbDiagSolID))
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
            ViewData["DiagnosticoID"] = new SelectList(_context.Diagnosticos, "DiagnosticoID", "DiagnosticoID", probDiagSol.DiagnosticoID);
            ViewData["ProblemaID"] = new SelectList(_context.Problemas, "ProblemaID", "ProblemaID", probDiagSol.ProblemaID);
            ViewData["ServicioID"] = new SelectList(_context.Servicios, "ServicioID", "ServicioID", probDiagSol.ServicioID);
            ViewData["SolucionID"] = new SelectList(_context.Soluciones, "SolucionID", "Descripcion", probDiagSol.SolucionID);
            return View(probDiagSol);
        }

        // GET: ProbDiagSoles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var probDiagSol = await _context.ProbDiagSoles
                .Include(p => p.Diagnostico)
                .Include(p => p.Problema)
                .Include(p => p.Servicio)
                .Include(p => p.Solucion)
                .FirstOrDefaultAsync(m => m.ProbDiagSolID == id);
            if (probDiagSol == null)
            {
                return NotFound();
            }

            return View(probDiagSol);
        }

        // POST: ProbDiagSoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var probDiagSol = await _context.ProbDiagSoles.FindAsync(id);
            _context.ProbDiagSoles.Remove(probDiagSol);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProbDiagSolExists(Guid id)
        {
            return _context.ProbDiagSoles.Any(e => e.ProbDiagSolID == id);
        }
    }
}
