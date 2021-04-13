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

            var rankingUsuarios = await (from sxu in _context.ServxUsuarios
                                         join srv in _context.Servicios on sxu.ServicioID equals srv.ServicioID
                                         where sxu.UsuarioID != null
                                         select new
                                         {
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
                                                    EnProceso = final.Where(p => p.Finalizado == false).Count(),
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
    }
}
