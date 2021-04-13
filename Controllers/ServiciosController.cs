using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    public class ServiciosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ServiciosController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Servicios
        public async Task<IActionResult> Index(string filter, int page = 1)
        {
            var lista = await _context.Servicios.Include(u => u.Cliente).ToListAsync();

            if (!string.IsNullOrEmpty(filter))
            {
                lista = lista.Where(c => c.Cliente.ApellidoNombre.Contains(filter)).ToList();
            }

            var servicios = PagingList.Create(lista.AsQueryable(), 10, page);

            servicios.RouteValue = new RouteValueDictionary { { "Filter", filter } };

            return View(servicios);
        }

        // GET: Servicios/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicios
                .FirstOrDefaultAsync(m => m.ServicioID == id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        // GET: Servicios/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: Servicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServicioID,ClienteID,EquipoID,FechaIng,Comentarios")] Servicio servicio)
        {
            if (ModelState.IsValid)
            {
                Cliente cliente = _context.Clientes.Find(servicio.ClienteID);

                servicio.ServicioID = Guid.NewGuid();
                servicio.FechaEgr = null;
                servicio.Cliente = cliente;
                servicio.TiempoTrabajo = 0;
                servicio.UnidadesTrabajo = 0;
                servicio.Total = 0;
                servicio.Solucionado = false;
                servicio.Finalizado = false;

                ServxUsuario servxUsuario = new ServxUsuario
                {
                    ServcUsuarioID = Guid.NewGuid(),
                    Servicio = servicio,
                    Status = false,
                    UsuarioID = null
                };

                //ProbDiagSol probDiagSol = new ProbDiagSol();
                //probDiagSol.ProbDiagSolID = Guid.NewGuid();
                //probDiagSol.Servicio = servicio;

                _context.Add(servicio);
                _context.Add(servxUsuario);
                //_context.Add(probDiagSol);

                await _context.SaveChangesAsync();
                return RedirectToAction("CompletarAltaServicio", "ProbDiagSoles",new { @id = servicio.ServicioID });
            }
            return View(servicio);
        }
        public async Task<IActionResult> Comentarios(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicios
                .Include(s => s.Cliente)
                .FirstOrDefaultAsync(m => m.ServicioID == id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        [HttpPost]
        public async Task<IActionResult> FinalizarServicio(Guid? id, bool solucionado = true)
        {

            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicios
                .FirstOrDefaultAsync(m => m.ServicioID == id);

            if (servicio == null)
            {
                return NotFound();
            }

            ServxUsuario servxusuario = _context.ServxUsuarios.Where(p => p.ServicioID == id && p.UsuarioID == _userManager.GetUserId(HttpContext.User)).FirstOrDefault();
            servxusuario.Status = true;

            servicio.Finalizado = true;
            servicio.Solucionado = solucionado;

            _context.Update(servxusuario);
            _context.Update(servicio);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index)); 
        }

        // GET: Servicios/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicios.FindAsync(id);
            List<Equipo> equipos = await _context.Equipos.ToListAsync();

            var filtrado = equipos.FindAll(m => m.ClienteID == servicio.ClienteID);
            ViewData["EquipoID"] = new SelectList(filtrado, "EquipoID", "Descripcion");

            if (servicio == null)
            {
                return NotFound();
            }
            return View(servicio);
        }

        // POST: Servicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ServicioID,EquipoID,FechaIng,FechaEgr,TiempoTrabajo,UnidadesTrabajo,Total,Comentarios,Solucionado")] Servicio servicio)
        {
            if (id != servicio.ServicioID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicioExists(servicio.ServicioID))
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
            return View(servicio);
        }

        // GET: Servicios/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicios
                .FirstOrDefaultAsync(m => m.ServicioID == id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        // POST: Servicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var servicio = await _context.Servicios.FindAsync(id);
            _context.Servicios.Remove(servicio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicioExists(Guid id)
        {
            return _context.Servicios.Any(e => e.ServicioID == id);
        }
    }
}
