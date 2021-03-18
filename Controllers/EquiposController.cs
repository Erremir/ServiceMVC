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
    public class EquiposController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EquiposController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Equipos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Equipos.Include(e => e.CategoriaEquipo).Include(e => e.Cliente);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Equipos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos
                .Include(e => e.CategoriaEquipo)
                .Include(e => e.Cliente)
                .FirstOrDefaultAsync(m => m.EquipoID == id);
            if (equipo == null)
            {
                return NotFound();
            }

            return View(equipo);
        }

        // GET: Equipos/Create
        public IActionResult Create()
        {
            ViewData["Partial"] = false;
            ViewData["CategoriaEquipoID"] = new SelectList(_context.CategoriaEquipos, "CategoriaEquipoID", "Categoria");
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteID", "ApellidoNombre");
            return View();
        }

        public IActionResult CreatePartial(Guid? id)
        {
            ViewData["Partial"] = true;
            ViewData["CategoriaEquipoID"] = new SelectList(_context.CategoriaEquipos, "CategoriaEquipoID", "Categoria");

            ViewData["ClienteID"] = id != null
                ? new SelectList(_context.Clientes, "ClienteID", "ApellidoNombre", id.ToString())
                : new SelectList(_context.Clientes, "ClienteID", "ApellidoNombre");

            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePartial([Bind("EquipoID,ClienteID,CategoriaEquipoID,Descripcion")] Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                equipo.EquipoID = Guid.NewGuid();
                _context.Add(equipo);
                await _context.SaveChangesAsync();

                var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.ClienteID == equipo.ClienteID);

                ViewBag.cliente = cliente;

                List<Equipo> equipos = await _context.Equipos.ToListAsync();

                var filtrado = equipos.FindAll(m => m.ClienteID == cliente.ClienteID);
                ViewData["EquipoID"] = new SelectList(filtrado, "EquipoID", "Descripcion");

                ViewData["Partial"] = true;

                return View("Views/Servicios/Create.cshtml");
            }
            
            ViewData["CategoriaEquipoID"] = new SelectList(_context.CategoriaEquipos, "CategoriaEquipoID", "Categoria", equipo.CategoriaEquipoID);
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteID", "ApellidoNombre", equipo.ClienteID);
                       
            return View(equipo);
        }

        // POST: Equipos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EquipoID,ClienteID,CategoriaEquipoID,Descripcion")] Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                equipo.EquipoID = Guid.NewGuid();
                _context.Add(equipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaEquipoID"] = new SelectList(_context.CategoriaEquipos, "CategoriaEquipoID", "Categoria", equipo.CategoriaEquipoID);
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteID", "ApellidoNombre", equipo.ClienteID);
            
            return View(equipo);
        }

        // GET: Equipos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo == null)
            {
                return NotFound();
            }
            ViewData["CategoriaEquipoID"] = new SelectList(_context.CategoriaEquipos, "CategoriaEquipoID", "Categoria", equipo.CategoriaEquipoID);
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteID", "ApellidoNombre", equipo.ClienteID);
            return View(equipo);
        }

        // POST: Equipos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EquipoID,ClienteID,CategoriaEquipoID,Descripcion")] Equipo equipo)
        {
            if (id != equipo.EquipoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipoExists(equipo.EquipoID))
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
            ViewData["CategoriaEquipoID"] = new SelectList(_context.CategoriaEquipos, "CategoriaEquipoID", "Categoria", equipo.CategoriaEquipoID);
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteID", "ApellidoNombre", equipo.ClienteID);
            return View(equipo);
        }

        // GET: Equipos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos
                .Include(e => e.CategoriaEquipo)
                .Include(e => e.Cliente)
                .FirstOrDefaultAsync(m => m.EquipoID == id);
            if (equipo == null)
            {
                return NotFound();
            }

            return View(equipo);
        }

        // POST: Equipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var equipo = await _context.Equipos.FindAsync(id);
            _context.Equipos.Remove(equipo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipoExists(Guid id)
        {
            return _context.Equipos.Any(e => e.EquipoID == id);
        }
    }
}
