using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using ServiceMVC.Data;
using ServiceMVC.Models;

namespace ServiceMVC.Controllers
{
    [Authorize]
    public class RankingClientessController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RankingClientessController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RankingClientess
        public async Task<IActionResult> Index(string filter, int page = 1, string sortExpression = "Apellido")
        {
            var serviciosClientes = await (from usr in _context.Servicios
                                           select new
                                           {
                                               ClienteID = usr.ClienteID,
                                               Cliente = usr.Cliente,
                                               TotalServicios = usr.Finalizado
                                           }).ToListAsync();

            List<RankingClientes> rankinclientes = (from srv in serviciosClientes
                                                          group srv by  srv.ClienteID into rnk
                                  select new RankingClientes()
                                  {
                                      RankingClientesID = Guid.NewGuid(),
                                      ClienteID = rnk.Key,
                                      Cliente = rnk.Select(c => c.Cliente).FirstOrDefault(),
                                      TotalServicios = rnk.Where(c => c.TotalServicios == true).Count()
                                  }).ToList();

            if (!string.IsNullOrEmpty(filter))
            {
                rankinclientes = rankinclientes.Where(p => p.Cliente.ApellidoNombre.Contains(filter)).ToList();
            }

            var ranking = PagingList.Create(rankinclientes.AsQueryable(), 10, page, sortExpression, "Solucionados");

            ranking.RouteValue = new RouteValueDictionary { { "Filter", filter } };

            return View(ranking);
        }

        // GET: RankingClientess/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rankingClientes = await _context.RankingClientess
                .Include(r => r.Cliente)
                .FirstOrDefaultAsync(m => m.RankingClientesID == id);
            if (rankingClientes == null)
            {
                return NotFound();
            }

            return View(rankingClientes);
        }

        // GET: RankingClientess/Create
        public IActionResult Create()
        {
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteID", "ClienteID");
            return View();
        }

        // POST: RankingClientess/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RankingClientesID,ClienteID,TotalServicios")] RankingClientes rankingClientes)
        {
            if (ModelState.IsValid)
            {
                rankingClientes.RankingClientesID = Guid.NewGuid();
                _context.Add(rankingClientes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteID", "ClienteID", rankingClientes.ClienteID);
            return View(rankingClientes);
        }

        // GET: RankingClientess/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rankingClientes = await _context.RankingClientess.FindAsync(id);
            if (rankingClientes == null)
            {
                return NotFound();
            }
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteID", "ClienteID", rankingClientes.ClienteID);
            return View(rankingClientes);
        }

        // POST: RankingClientess/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RankingClientesID,ClienteID,TotalServicios")] RankingClientes rankingClientes)
        {
            if (id != rankingClientes.RankingClientesID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rankingClientes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RankingClientesExists(rankingClientes.RankingClientesID))
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
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteID", "ClienteID", rankingClientes.ClienteID);
            return View(rankingClientes);
        }

        // GET: RankingClientess/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rankingClientes = await _context.RankingClientess
                .Include(r => r.Cliente)
                .FirstOrDefaultAsync(m => m.RankingClientesID == id);
            if (rankingClientes == null)
            {
                return NotFound();
            }

            return View(rankingClientes);
        }

        // POST: RankingClientess/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var rankingClientes = await _context.RankingClientess.FindAsync(id);
            _context.RankingClientess.Remove(rankingClientes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RankingClientesExists(Guid id)
        {
            return _context.RankingClientess.Any(e => e.RankingClientesID == id);
        }
    }
}
