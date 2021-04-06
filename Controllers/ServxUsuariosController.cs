using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceMVC.Data;
using ServiceMVC.Models;

namespace ServiceMVC.Controllers
{
    [Authorize]
    public class ServxUsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ServxUsuariosController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ServxUsuarios
        public async Task<IActionResult> Index()
        {

            string userID = _userManager.GetUserId(HttpContext.User);
            var applicationDbContext = _context.ServxUsuarios
                .Include(s => s.Servicio).Include(c => c.Servicio.Cliente)
                .Include(s => s.Usuario)
                .Where(u => u.UsuarioID == userID && u.Servicio.Finalizado == false)
                .Where(s => s.Status == false);

            ViewBag.servdisp = _context.ServxUsuarios.Where(u => u.UsuarioID == null).Count();

            return View(await applicationDbContext.ToListAsync());
        }

        //GET: ServxUsuarios/AllServices
        public async Task<IActionResult> AllServices()
        {
            var applicationDbContext = _context.ServxUsuarios
                .Include(s => s.Servicio).Include(c => c.Servicio.Cliente)
                .Include(s => s.Usuario)
                .Where(s => s.Servicio.Finalizado == false);
            return View("Index", await applicationDbContext.ToListAsync());
        }

        //GET: ServxUsuarios/AvaiableServices
        public async Task<IActionResult> AvaiableServices()
        {
            var applicationDbContext = _context.ServxUsuarios
                .Include(s => s.Servicio).Include(c => c.Servicio.Cliente)
                .Include(s => s.Usuario).Where(u => u.UsuarioID == null)
                .Where(s => s.Status == false);
            return View("Index", await applicationDbContext.ToListAsync());
        }

        //GET: ServxUsuarios/TakeService/1
        public async Task<IActionResult> TakeService(Guid id)
        {
            string userID = _userManager.GetUserId(HttpContext.User);
            var servxUsuario = await _context.ServxUsuarios.FirstOrDefaultAsync(m => m.ServcUsuarioID == id);
            servxUsuario.UsuarioID = userID;
            
            _context.Update(servxUsuario);
            await _context.SaveChangesAsync();

            var applicationDbContext = _context.ServxUsuarios.Include(s => s.Servicio).Include(s => s.Usuario).Where(u => u.UsuarioID == userID);
            return View("Index", await applicationDbContext.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AbandonarServicio(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servxusuario = await _context.ServxUsuarios
                .FirstOrDefaultAsync(m => m.ServicioID == id && m.UsuarioID == _userManager.GetUserId(HttpContext.User));

            if (servxusuario == null)
            {
                return NotFound();
            }

            servxusuario.UsuarioID = null;
            servxusuario.Usuario = null;

            _context.Update(servxusuario);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index), "ServxUsuarios");
        }

        // GET: ServxUsuarios/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servxUsuario = await _context.ServxUsuarios
                .Include(s => s.Servicio).Include(c => c.Servicio.Cliente)
                .Include(s => s.Usuario)
                .FirstOrDefaultAsync(m => m.ServcUsuarioID == id);
            if (servxUsuario == null)
            {
                return NotFound();
            }

            return View(servxUsuario);
        }

        // GET: ServxUsuarios/Create
        public IActionResult Create()
        {
            ViewData["ServicioID"] = new SelectList(_context.Servicios, "ServicioID", "ServicioID");
            ViewData["UsuarioID"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: ServxUsuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServcUsuarioID,UsuarioID,ServicioID,Status")] ServxUsuario servxUsuario)
        {
            if (ModelState.IsValid)
            {
                servxUsuario.ServcUsuarioID = Guid.NewGuid();
                _context.Add(servxUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServicioID"] = new SelectList(_context.Servicios, "ServicioID", "ServicioID", servxUsuario.ServicioID);
            ViewData["UsuarioID"] = new SelectList(_context.Users, "Id", "UserName", servxUsuario.UsuarioID);
            return View(servxUsuario);
        }

        // GET: ServxUsuarios/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servxUsuario = await _context.ServxUsuarios.FindAsync(id);
            if (servxUsuario == null)
            {
                return NotFound();
            }
            ViewData["ServicioID"] = new SelectList(_context.Servicios, "ServicioID", "ServicioID", servxUsuario.ServicioID);
            ViewData["UsuarioID"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", servxUsuario.UsuarioID);
            return View(servxUsuario);
        }

        // POST: ServxUsuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ServcUsuarioID,UsuarioID,ServicioID,Status")] ServxUsuario servxUsuario)
        {
            if (id != servxUsuario.ServcUsuarioID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servxUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServxUsuarioExists(servxUsuario.ServcUsuarioID))
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
            ViewData["ServicioID"] = new SelectList(_context.Servicios, "ServicioID", "ServicioID", servxUsuario.ServicioID);
            ViewData["UsuarioID"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", servxUsuario.UsuarioID);
            return View(servxUsuario);
        }

        // GET: ServxUsuarios/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servxUsuario = await _context.ServxUsuarios
                .Include(s => s.Servicio).Include(c => c.Servicio.Cliente)
                .Include(s => s.Usuario)
                .FirstOrDefaultAsync(m => m.ServcUsuarioID == id);
            if (servxUsuario == null)
            {
                return NotFound();
            }

            return View(servxUsuario);
        }

        // POST: ServxUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var servxUsuario = await _context.ServxUsuarios.FindAsync(id);
            _context.ServxUsuarios.Remove(servxUsuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServxUsuarioExists(Guid id)
        {
            return _context.ServxUsuarios.Any(e => e.ServcUsuarioID == id);
        }
    }
}
