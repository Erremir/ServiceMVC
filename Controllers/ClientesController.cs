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
    public class ClientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index(string filter, int page = 1,string sortExpression = "Apellido")
        {
            List<Cliente> lista = new List<Cliente>();

            if (!string.IsNullOrEmpty(filter))
            {
                lista = await _context.Clientes
                    .Where(c => c.Status == true)
                    .Where(c => c.Apellido.Contains(filter) || c.Nombre.Contains(filter)).ToListAsync();
            }
            else
            {
                lista = await _context.Clientes.Where(c => c.Status == true).ToListAsync();
            }

            var clientes = PagingList.Create(lista.AsQueryable(), 10, page, sortExpression, "Apellido");

            clientes.RouteValue = new RouteValueDictionary { { "Filter", filter } };

            return View(clientes);
        }

        public async Task<IActionResult> IndexAll(string filter, int page = 1, string sortExpression = "Apellido")
        {
            List<Cliente> lista = new List<Cliente>();

            if (!string.IsNullOrEmpty(filter))
            {
                lista = await _context.Clientes.Where(c => c.Apellido.Contains(filter) || c.Nombre.Contains(filter)).ToListAsync();
            }
            else
            {
                lista = await _context.Clientes.ToListAsync();
            }

            var clientes = PagingList.Create(lista.AsQueryable(), 10, page, sortExpression, "Apellido");

            clientes.RouteValue = new RouteValueDictionary { { "Filtro", filter } };

            return View("Index", clientes);
        }

        public async Task<IActionResult> BuscarClientes()
        {
            return PartialView("_Clientes", await _context.Clientes.ToListAsync());
        }


        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.ClienteID == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        public async Task<IActionResult> Comentario(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.ClienteID == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        public async Task<IActionResult> ClienteDetalle(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.ClienteID == id);
            if (cliente == null)
            {
                return NotFound();
            }

            ViewBag.cliente = cliente;
            List<Equipo> equipos = await _context.Equipos.ToListAsync();
            var filtrado = equipos.FindAll(m => m.ClienteID == cliente.ClienteID);
            ViewData["EquipoID"] = new SelectList(filtrado, "EquipoID", "Descripcion");

            return View("Views/Servicios/Create.cshtml");
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteID,Nombre,Apellido,Domicilio,Email,TelCel,TelFijo,Comentario")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.ClienteID = Guid.NewGuid();
                cliente.Status = true;
                _context.Add(cliente);
                
                RankingClientes rankingClientes = new RankingClientes();
                rankingClientes.RankingClientesID = Guid.NewGuid();
                rankingClientes.ClienteID = cliente.ClienteID;
                rankingClientes.TotalServicios = 0;
                _context.Add(rankingClientes);
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ClienteID,Nombre,Apellido,Domicilio,Email,TelCel,TelFijo,Comentario,Status")] Cliente cliente)
        {
            if (id != cliente.ClienteID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.ClienteID))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.ClienteID == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            cliente.Status = false;
            _context.Update(cliente);
            //_context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(Guid id)
        {
            return _context.Clientes.Any(e => e.ClienteID == id);
        }
    }
}
