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
    public class CategoriaEquiposController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriaEquiposController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CategoriaEquipos
        public async Task<IActionResult> Index()
        {
            return View(await _context.CategoriaEquipos.ToListAsync());
        }

        // GET: CategoriaEquipos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaEquipo = await _context.CategoriaEquipos
                .FirstOrDefaultAsync(m => m.CategoriaEquipoID == id);
            if (categoriaEquipo == null)
            {
                return NotFound();
            }

            return View(categoriaEquipo);
        }

        // GET: CategoriaEquipos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriaEquipos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoriaEquipoID,Categoria")] CategoriaEquipo categoriaEquipo)
        {
            if (ModelState.IsValid)
            {
                categoriaEquipo.CategoriaEquipoID = Guid.NewGuid();
                _context.Add(categoriaEquipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaEquipo);
        }

        // GET: CategoriaEquipos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaEquipo = await _context.CategoriaEquipos.FindAsync(id);
            if (categoriaEquipo == null)
            {
                return NotFound();
            }
            return View(categoriaEquipo);
        }

        // POST: CategoriaEquipos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CategoriaEquipoID,Categoria")] CategoriaEquipo categoriaEquipo)
        {
            if (id != categoriaEquipo.CategoriaEquipoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriaEquipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaEquipoExists(categoriaEquipo.CategoriaEquipoID))
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
            return View(categoriaEquipo);
        }

        // GET: CategoriaEquipos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaEquipo = await _context.CategoriaEquipos
                .FirstOrDefaultAsync(m => m.CategoriaEquipoID == id);
            if (categoriaEquipo == null)
            {
                return NotFound();
            }

            return View(categoriaEquipo);
        }

        // POST: CategoriaEquipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var categoriaEquipo = await _context.CategoriaEquipos.FindAsync(id);
            _context.CategoriaEquipos.Remove(categoriaEquipo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaEquipoExists(Guid id)
        {
            return _context.CategoriaEquipos.Any(e => e.CategoriaEquipoID == id);
        }
    }
}
