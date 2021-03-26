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
    public class RankingClientessController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RankingClientessController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RankingClientess
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RankingClientess.Include(r => r.Cliente).Where(r => r.Cliente.Status == true).OrderByDescending(r => r.TotalServicios);
            return View(await applicationDbContext.ToListAsync());
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
