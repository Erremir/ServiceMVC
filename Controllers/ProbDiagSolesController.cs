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
    public class ProbDiagSolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProbDiagSolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProbDiagSoles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProbDiagSoles.Include(p => p.Diagnostico).Include(p => p.Problema).Include(p => p.Servicio).Include(p => p.Solucion);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProbDiagSoles/CompletarAltaServicio/1
        public async Task<IActionResult> CompletarAltaServicio(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Servicio servicio = _context.Servicios.Find(id);
            ViewBag.Servicio = servicio;
            ViewBag.Cliente = _context.Clientes.Find(servicio.ClienteID).ApellidoNombre;
            ViewBag.Equipo = _context.Equipos.Find(servicio.EquipoID);
 
            var applicationDbContext = await _context.ProbDiagSoles
                .Include(p => p.Diagnostico)
                .Include(p => p.Problema)
                .Include(p => p.Servicio)
                .Include(p => p.Servicio.Cliente)
                .Include(p => p.Solucion)
                .Where(p => p.ServicioID == id).ToListAsync();

            ViewBag.TotalProbs = applicationDbContext.Where(p => p.Problema != null).Count();
            ViewBag.TotalDiags = applicationDbContext.Where(p => p.Diagnostico != null).Count();
            ViewBag.TotalSoles = applicationDbContext.Where(p => p.Solucion != null).Count();

            if (applicationDbContext.Select(p => p.Problema).Count() == 0)
            {
                ViewBag.Title = "Completal Alta";
            }
            else
            {
                ViewBag.Title = "Procesar Servicio";
            }

            return View(applicationDbContext);
        }
//Agregar Problema
        public IActionResult AgregarProb(Guid id)
        {
            ViewBag.ServicioID = id;
            ViewData["ProblemaID"] = new SelectList(_context.Problemas, "ProblemaID", "Descripcion");
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VincularProb([Bind("ServicioID,ProblemaID")] ProbDiagSol probDiagSol)
        {

            if (ModelState.IsValid)
            {
                probDiagSol.ProbDiagSolID = Guid.NewGuid();
                _context.Add(probDiagSol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CompletarAltaServicio), new { @id = probDiagSol.ServicioID });
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AgregarProb( string Descripcion, [Bind("ServicioID")] ProbDiagSol probDiagSol)
        {

            if (ModelState.IsValid)
            {
                Problema problema = new Problema();
                problema.ProblemaID = Guid.NewGuid();
                problema.Descripcion = Descripcion;

                probDiagSol.ProbDiagSolID = Guid.NewGuid();
                probDiagSol.ProblemaID = problema.ProblemaID;
                probDiagSol.Problema = problema;
                
                _context.Add(problema);
                _context.Add(probDiagSol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CompletarAltaServicio), new { @id = probDiagSol.ServicioID });
            }

            return View();
        }

        //AgregarDiag
        public IActionResult AgregarDiag(Guid id)
        {
            ViewBag.ServicioID = id;
            List<Problema> problemas = _context.ProbDiagSoles.Where(p => p.ServicioID == id).Select(p => p.Problema).ToList();
            ViewData["ProblemaID"] = new SelectList(problemas, "ProblemaID", "Descripcion");

            //List<Diagnostico> diagnosticos = _context.ProbxDiags.Where(p => p.ServicioID == id).Select(p => p.Problema).ToList();
            ViewData["DiagnosticoID"] = new SelectList(_context.Diagnosticos, "DiagnosticoID", "Descripcion");

            return View();
        }

        public IActionResult ProblemaChange(Guid id)
        {
            var diagnosticos = _context.ProbxDiags.Where(p => p.ProblemaID == id).Select(p => p.Diagnostico);
            return Json(diagnosticos);
        }

        [HttpPost]
        public async Task<IActionResult> VincularDiag([Bind("ServicioID,ProblemaID, DiagnosticoID")] ProbDiagSol probDiagSol)
        {

            if (ModelState.IsValid)
            {
                ProbDiagSol probDiagSolupdate = _context.ProbDiagSoles.Where(p => p.ServicioID == probDiagSol.ServicioID && p.ProblemaID == probDiagSol.ProblemaID).FirstOrDefault();
                probDiagSolupdate.DiagnosticoID = probDiagSol.DiagnosticoID;

                ProbxDiag probxDiag = _context.ProbxDiags.Where(p => p.ProblemaID == probDiagSolupdate.ProblemaID && p.DiagnosticoID == probDiagSolupdate.DiagnosticoID).FirstOrDefault();
                if (probxDiag == null)
                {
                    ProbxDiag newProbxDiag = new ProbxDiag();
                    newProbxDiag.ProbxDiagID = Guid.NewGuid();
                    newProbxDiag.ProblemaID = (Guid)probDiagSolupdate.ProblemaID;
                    newProbxDiag.DiagnosticoID = (Guid)probDiagSolupdate.DiagnosticoID;
                    _context.Add(newProbxDiag);
                }

                _context.Update(probDiagSolupdate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CompletarAltaServicio), new { @id = probDiagSol.ServicioID });
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AgregarDiag(string Descripcion, [Bind("ServicioID, ProblemaID")] ProbDiagSol probDiagSol)
        {

            if (ModelState.IsValid)
            {
                Diagnostico diagnostico = new Diagnostico();
                diagnostico.DiagnosticoID = Guid.NewGuid();
                diagnostico.Descripcion = Descripcion;

                ProbDiagSol probDiagSolupdate = _context.ProbDiagSoles.Where(p => p.ServicioID == probDiagSol.ServicioID && p.ProblemaID == probDiagSol.ProblemaID).FirstOrDefault();
                probDiagSolupdate.DiagnosticoID = diagnostico.DiagnosticoID;
                probDiagSolupdate.Diagnostico = diagnostico;

                ProbxDiag probxDiag = _context.ProbxDiags.Where(p => p.ProblemaID == probDiagSolupdate.ProblemaID && p.DiagnosticoID == probDiagSolupdate.DiagnosticoID).FirstOrDefault();
                if(probxDiag == null)
                {
                    ProbxDiag newProbxDiag = new ProbxDiag();
                    newProbxDiag.ProbxDiagID = Guid.NewGuid();
                    newProbxDiag.ProblemaID = (Guid)probDiagSolupdate.ProblemaID;
                    newProbxDiag.DiagnosticoID = (Guid)probDiagSolupdate.DiagnosticoID;
                    _context.Add(newProbxDiag);
                }

                _context.Add(diagnostico);
                _context.Update(probDiagSolupdate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CompletarAltaServicio), new { @id = probDiagSol.ServicioID });
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CompletarAltaServicio([Bind("ServicioID,ProblemaID,DiagnosticoID,SolucionID")] ProbDiagSol probDiagSol)
        {
            if (ModelState.IsValid)
            {
                probDiagSol.ProbDiagSolID = Guid.NewGuid();
                _context.Add(probDiagSol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CompletarAltaServicio), new { @id = probDiagSol.ServicioID });
            }

            ViewData["DiagnosticoID"] = new SelectList(_context.Diagnosticos, "DiagnosticoID", "Descripcion", probDiagSol.DiagnosticoID);
            ViewData["ProblemaID"] = new SelectList(_context.Problemas, "ProblemaID", "Descripcion", probDiagSol.ProblemaID);
            ViewData["ServicioID"] = new SelectList(_context.Servicios, "ServicioID", "ServicioID", probDiagSol.ServicioID);
            ViewData["SolucionID"] = new SelectList(_context.Soluciones, "SolucionID", "Descripcion", probDiagSol.SolucionID);
            return View(probDiagSol);
        }

        // GET: ProbDiagSoles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var probDiagSol = await _context.ProbDiagSoles
                .Include(p => p.Diagnostico)
                .Include(p => p.Problema)
                .Include(p => p.Servicio)
                .Include(p => p.Solucion)
                .FirstOrDefaultAsync(m => m.ProbDiagSolID == id);
            if (probDiagSol == null)
            {
                return NotFound();
            }

            return View(probDiagSol);
        }

        // GET: ProbDiagSoles/Create
        public IActionResult Create()
        {
            ViewData["DiagnosticoID"] = new SelectList(_context.Diagnosticos, "DiagnosticoID", "Descripcion");
            ViewData["ProblemaID"] = new SelectList(_context.Problemas, "ProblemaID", "Descripcion");
            ViewData["ServicioID"] = new SelectList(_context.Servicios, "ServicioID", "ServicioID");
            ViewData["SolucionID"] = new SelectList(_context.Soluciones, "SolucionID", "Descripcion");
            return View();
        }

        // POST: ProbDiagSoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProbDiagSolID,ServicioID,ProblemaID,DiagnosticoID,SolucionID")] ProbDiagSol probDiagSol)
        {
            if (ModelState.IsValid)
            {
                probDiagSol.ProbDiagSolID = Guid.NewGuid();
                _context.Add(probDiagSol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiagnosticoID"] = new SelectList(_context.Diagnosticos, "DiagnosticoID", "Descripcion", probDiagSol.DiagnosticoID);
            ViewData["ProblemaID"] = new SelectList(_context.Problemas, "ProblemaID", "Descripcion", probDiagSol.ProblemaID);
            ViewData["ServicioID"] = new SelectList(_context.Servicios, "ServicioID", "ServicioID", probDiagSol.ServicioID);
            ViewData["SolucionID"] = new SelectList(_context.Soluciones, "SolucionID", "Descripcion", probDiagSol.SolucionID);
            return View(probDiagSol);
        }

        // GET: ProbDiagSoles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var probDiagSol = await _context.ProbDiagSoles.FindAsync(id);
            if (probDiagSol == null)
            {
                return NotFound();
            }
            ViewData["DiagnosticoID"] = new SelectList(_context.Diagnosticos, "DiagnosticoID", "Descripcion", probDiagSol.DiagnosticoID);
            ViewData["ProblemaID"] = new SelectList(_context.Problemas, "ProblemaID", "Descripcion", probDiagSol.ProblemaID);
            ViewData["ServicioID"] = new SelectList(_context.Servicios, "ServicioID", "ServicioID", probDiagSol.ServicioID);
            ViewData["SolucionID"] = new SelectList(_context.Soluciones, "SolucionID", "Descripcion", probDiagSol.SolucionID);
            return View(probDiagSol);
        }

        // POST: ProbDiagSoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProbDiagSolID,ServicioID,ProblemaID,DiagnosticoID,SolucionID")] ProbDiagSol probDiagSol)
        {
            if (id != probDiagSol.ProbDiagSolID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(probDiagSol);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProbDiagSolExists(probDiagSol.ProbDiagSolID))
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
            ViewData["DiagnosticoID"] = new SelectList(_context.Diagnosticos, "DiagnosticoID", "Descripcion", probDiagSol.DiagnosticoID);
            ViewData["ProblemaID"] = new SelectList(_context.Problemas, "ProblemaID", "Descripcion", probDiagSol.ProblemaID);
            ViewData["ServicioID"] = new SelectList(_context.Servicios, "ServicioID", "ServicioID", probDiagSol.ServicioID);
            ViewData["SolucionID"] = new SelectList(_context.Soluciones, "SolucionID", "Descripcion", probDiagSol.SolucionID);
            return View(probDiagSol);
        }

        // GET: ProbDiagSoles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var probDiagSol = await _context.ProbDiagSoles
                .Include(p => p.Diagnostico)
                .Include(p => p.Problema)
                .Include(p => p.Servicio)
                .Include(p => p.Solucion)
                .FirstOrDefaultAsync(m => m.ProbDiagSolID == id);
            if (probDiagSol == null)
            {
                return NotFound();
            }

            return View(probDiagSol);
        }

        // POST: ProbDiagSoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var probDiagSol = await _context.ProbDiagSoles.FindAsync(id);
            _context.ProbDiagSoles.Remove(probDiagSol);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProbDiagSolExists(Guid id)
        {
            return _context.ProbDiagSoles.Any(e => e.ProbDiagSolID == id);
        }
    }
}
