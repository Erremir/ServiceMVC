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
    public class ArancelTiemposController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArancelTiemposController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ArancelTiempos
        public async Task<IActionResult> Index()
        {
            return View(await _context.ArancelTiempos.ToListAsync());
        }

        // GET: ArancelTiempos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arancelTiempo = await _context.ArancelTiempos
                .FirstOrDefaultAsync(m => m.ArancelTiempoID == id);
            if (arancelTiempo == null)
            {
                return NotFound();
            }

            return View(arancelTiempo);
        }

        // GET: ArancelTiempos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ArancelTiempos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArancelTiempoID,Precio")] ArancelTiempo arancelTiempo)
        {
            if (ModelState.IsValid)
            {
                arancelTiempo.ArancelTiempoID = Guid.NewGuid();
                _context.Add(arancelTiempo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(arancelTiempo);
        }

        // GET: ArancelTiempos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arancelTiempo = await _context.ArancelTiempos.FindAsync(id);
            if (arancelTiempo == null)
            {
                return NotFound();
            }
            return View(arancelTiempo);
        }

        // POST: ArancelTiempos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ArancelTiempoID,Precio")] ArancelTiempo arancelTiempo)
        {
            if (id != arancelTiempo.ArancelTiempoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(arancelTiempo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArancelTiempoExists(arancelTiempo.ArancelTiempoID))
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
            return View(arancelTiempo);
        }

        // GET: ArancelTiempos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arancelTiempo = await _context.ArancelTiempos
                .FirstOrDefaultAsync(m => m.ArancelTiempoID == id);
            if (arancelTiempo == null)
            {
                return NotFound();
            }

            return View(arancelTiempo);
        }

        // POST: ArancelTiempos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var arancelTiempo = await _context.ArancelTiempos.FindAsync(id);
            _context.ArancelTiempos.Remove(arancelTiempo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArancelTiempoExists(Guid id)
        {
            return _context.ArancelTiempos.Any(e => e.ArancelTiempoID == id);
        }
    }
}
