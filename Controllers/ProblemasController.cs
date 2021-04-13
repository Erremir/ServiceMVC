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
    public class ProblemasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProblemasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Problemas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Problemas.ToListAsync());
        }

        // GET: Problemas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problema = await _context.Problemas
                .FirstOrDefaultAsync(m => m.ProblemaID == id);
            if (problema == null)
            {
                return NotFound();
            }

            return View(problema);
        }

        // GET: Problemas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Problemas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProblemaID,Descripcion")] Problema problema)
        {
            if (ModelState.IsValid)
            {
                problema.ProblemaID = Guid.NewGuid();
                _context.Add(problema);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(problema);
        }

        // GET: Problemas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problema = await _context.Problemas.FindAsync(id);
            if (problema == null)
            {
                return NotFound();
            }
            return View(problema);
        }

        // POST: Problemas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProblemaID,Descripcion")] Problema problema)
        {
            if (id != problema.ProblemaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(problema);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProblemaExists(problema.ProblemaID))
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
            return View(problema);
        }

        // GET: Problemas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problema = await _context.Problemas
                .FirstOrDefaultAsync(m => m.ProblemaID == id);
            if (problema == null)
            {
                return NotFound();
            }

            return View(problema);
        }

        // POST: Problemas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var problema = await _context.Problemas.FindAsync(id);
            _context.Problemas.Remove(problema);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProblemaExists(Guid id)
        {
            return _context.Problemas.Any(e => e.ProblemaID == id);
        }
    }
}
