using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
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
        public async Task<IActionResult> Index(string filter, int page = 1, string sortExpression = "Solucionados")
        {
            //var applicationDbContext = _context.RankingUsuarios.Include(r => r.Usuario);

            var rankingUsuarios = await (from sxu in _context.ServxUsuarios
                                                          join srv in _context.Servicios on sxu.ServicioID equals srv.ServicioID
                                                          where sxu.UsuarioID != null
                                                          select new { 
                                                    Usuario = sxu.Usuario,
                                                    UsuarioID = sxu.UsuarioID,
                                                    Finalizado = sxu.Servicio.Finalizado,                                              
                                                    Solucionados = sxu.Servicio.Solucionado
                                                    }).ToListAsync();

            List<RankingUsuario> rankingprev = (from rank in rankingUsuarios
                           group rank by rank.UsuarioID into final
                           join usr in _context.Users on final.Key equals usr.Id
                           select new RankingUsuario()
                           {
                               RankingUsuarioID = Guid.NewGuid(),
                               UsuarioID = final.Key,
                               Usuario = (ApplicationUser)usr,
                               EnProceso  = final.Where(p => p.Finalizado == false).Count(),
                               Fallados = final.Where(p => p.Finalizado == true && p.Solucionados == false).Count(),
                               Solucionados = final.Where(p => p.Finalizado == true && p.Solucionados == true).Count()
                           }).ToList();
            
            if (!string.IsNullOrEmpty(filter))
            {
                rankingprev = rankingprev.Where(p => p.Usuario.UserName.Contains(filter)).ToList();
            }

            var ranking = PagingList.Create(rankingprev.AsQueryable(), 10, page, sortExpression, "Solucionados");

            ranking.RouteValue = new RouteValueDictionary { { "Filter", filter } };

            return View(ranking);
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
