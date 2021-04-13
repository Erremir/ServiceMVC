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
    public class ArancelUnidadesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArancelUnidadesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ArancelUnidades
        public async Task<IActionResult> Index()
        {
            return View(await _context.ArancelUnidades.ToListAsync());
        }

        // GET: ArancelUnidades/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arancelUnidad = await _context.ArancelUnidades
                .FirstOrDefaultAsync(m => m.ArancelUnidadID == id);
            if (arancelUnidad == null)
            {
                return NotFound();
            }

            return View(arancelUnidad);
        }

        // GET: ArancelUnidades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ArancelUnidades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArancelUnidadID,Precio")] ArancelUnidad arancelUnidad)
        {
            if (ModelState.IsValid)
            {
                arancelUnidad.ArancelUnidadID = Guid.NewGuid();
                _context.Add(arancelUnidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(arancelUnidad);
        }

        // GET: ArancelUnidades/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arancelUnidad = await _context.ArancelUnidades.FindAsync(id);
            if (arancelUnidad == null)
            {
                return NotFound();
            }
            return View(arancelUnidad);
        }

        // POST: ArancelUnidades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ArancelUnidadID,Precio")] ArancelUnidad arancelUnidad)
        {
            if (id != arancelUnidad.ArancelUnidadID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(arancelUnidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArancelUnidadExists(arancelUnidad.ArancelUnidadID))
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
            return View(arancelUnidad);
        }

        // GET: ArancelUnidades/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arancelUnidad = await _context.ArancelUnidades
                .FirstOrDefaultAsync(m => m.ArancelUnidadID == id);
            if (arancelUnidad == null)
            {
                return NotFound();
            }

            return View(arancelUnidad);
        }

        // POST: ArancelUnidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var arancelUnidad = await _context.ArancelUnidades.FindAsync(id);
            _context.ArancelUnidades.Remove(arancelUnidad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArancelUnidadExists(Guid id)
        {
            return _context.ArancelUnidades.Any(e => e.ArancelUnidadID == id);
        }
    }
}
