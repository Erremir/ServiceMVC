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
    public class CostosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CostosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Costos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Costos.Include(c => c.Solucion);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Costos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costo = await _context.Costos
                .Include(c => c.Solucion)
                .FirstOrDefaultAsync(m => m.CostoID == id);
            if (costo == null)
            {
                return NotFound();
            }

            return View(costo);
        }

        // GET: Costos/Create
        public IActionResult Create()
        {
            ViewData["SolucionID"] = new SelectList(_context.Soluciones, "SolucionID", "Descripcion");
            return View();
        }

        // POST: Costos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CostoID,SolucionID,Tiempo,Unidades")] Costo costo)
        {
            if (ModelState.IsValid)
            {
                costo.CostoID = Guid.NewGuid();
                _context.Add(costo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SolucionID"] = new SelectList(_context.Soluciones, "SolucionID", "Descripcion", costo.SolucionID);
            return View(costo);
        }

        // GET: Costos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costo = await _context.Costos.FindAsync(id);
            if (costo == null)
            {
                return NotFound();
            }
            ViewData["SolucionID"] = new SelectList(_context.Soluciones, "SolucionID", "Descripcion", costo.SolucionID);
            return View(costo);
        }

        // POST: Costos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CostoID,SolucionID,Tiempo,Unidades")] Costo costo)
        {
            if (id != costo.CostoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(costo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CostoExists(costo.CostoID))
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
            ViewData["SolucionID"] = new SelectList(_context.Soluciones, "SolucionID", "Descripcion", costo.SolucionID);
            return View(costo);
        }

        // GET: Costos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costo = await _context.Costos
                .Include(c => c.Solucion)
                .FirstOrDefaultAsync(m => m.CostoID == id);
            if (costo == null)
            {
                return NotFound();
            }

            return View(costo);
        }

        // POST: Costos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var costo = await _context.Costos.FindAsync(id);
            _context.Costos.Remove(costo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CostoExists(Guid id)
        {
            return _context.Costos.Any(e => e.CostoID == id);
        }
    }
}
