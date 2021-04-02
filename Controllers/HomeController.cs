using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using ServiceMVC.Data;
using ServiceMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteID", "ApellidoNombre");
            return View();
        }

        public IActionResult Pruebas()
        {
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteID", "ApellidoNombre");
            return View();
        }

        public IActionResult ClientChange(Guid id)
        {
            var equipos = _context.Equipos.Where(p => p.ClienteID == id);
            return Json( equipos );
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
