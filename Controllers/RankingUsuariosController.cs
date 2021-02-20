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
    public class RankingUsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RankingUsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RankingUsuarios
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RankingUsuarios.Include(r => r.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RankingUsuarios/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rankingUsuario = await _context.RankingUsuarios
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.RankingUsuarioID == id);
            if (rankingUsuario == null)
            {
                return NotFound();
            }

            return View(rankingUsuario);
        }

        // GET: RankingUsuarios/Create
        public IActionResult Create()
        {
            ViewData["UsuarioID"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id");
            return View();
        }

        // POST: RankingUsuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RankingUsuarioID,UsuarioID,EnProceso,Solucionados,Fallados")] RankingUsuario rankingUsuario)
        {
            if (ModelState.IsValid)
            {
                rankingUsuario.RankingUsuarioID = Guid.NewGuid();
                _context.Add(rankingUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioID"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", rankingUsuario.UsuarioID);
            return View(rankingUsuario);
        }

        // GET: RankingUsuarios/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rankingUsuario = await _context.RankingUsuarios.FindAsync(id);
            if (rankingUsuario == null)
            {
                return NotFound();
            }
            ViewData["UsuarioID"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", rankingUsuario.UsuarioID);
            return View(rankingUsuario);
        }

        // POST: RankingUsuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RankingUsuarioID,UsuarioID,EnProceso,Solucionados,Fallados")] RankingUsuario rankingUsuario)
        {
            if (id != rankingUsuario.RankingUsuarioID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rankingUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RankingUsuarioExists(rankingUsuario.RankingUsuarioID))
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
            ViewData["UsuarioID"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", rankingUsuario.UsuarioID);
            return View(rankingUsuario);
        }

        // GET: RankingUsuarios/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rankingUsuario = await _context.RankingUsuarios
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.RankingUsuarioID == id);
            if (rankingUsuario == null)
            {
                return NotFound();
            }

            return View(rankingUsuario);
        }

        // POST: RankingUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var rankingUsuario = await _context.RankingUsuarios.FindAsync(id);
            _context.RankingUsuarios.Remove(rankingUsuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RankingUsuarioExists(Guid id)
        {
            return _context.RankingUsuarios.Any(e => e.RankingUsuarioID == id);
        }
    }
}
