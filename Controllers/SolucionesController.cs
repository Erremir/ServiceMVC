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
    public class SolucionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SolucionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Soluciones
        public async Task<IActionResult> Index()
        {
            return View(await _context.Soluciones.ToListAsync());
        }

        // GET: Soluciones/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solucion = await _context.Soluciones
                .FirstOrDefaultAsync(m => m.SolucionID == id);
            if (solucion == null)
            {
                return NotFound();
            }

            return View(solucion);
        }

        // GET: Soluciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Soluciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SolucionID,Descripcion")] Solucion solucion)
        {
            if (ModelState.IsValid)
            {
                solucion.SolucionID = Guid.NewGuid();
                _context.Add(solucion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(solucion);
        }

        // GET: Soluciones/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solucion = await _context.Soluciones.FindAsync(id);
            if (solucion == null)
            {
                return NotFound();
            }
            return View(solucion);
        }

        // POST: Soluciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SolucionID,Descripcion")] Solucion solucion)
        {
            if (id != solucion.SolucionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solucion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolucionExists(solucion.SolucionID))
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
            return View(solucion);
        }

        // GET: Soluciones/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solucion = await _context.Soluciones
                .FirstOrDefaultAsync(m => m.SolucionID == id);
            if (solucion == null)
            {
                return NotFound();
            }

            return View(solucion);
        }

        // POST: Soluciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var solucion = await _context.Soluciones.FindAsync(id);
            _context.Soluciones.Remove(solucion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolucionExists(Guid id)
        {
            return _context.Soluciones.Any(e => e.SolucionID == id);
        }
    }
}
