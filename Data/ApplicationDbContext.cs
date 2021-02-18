using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceMVC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ArancelTiempo> ArancelTiempos  { get; set; }
        public DbSet<ArancelUnidad> ArancelUnidades { get; set; }
        public DbSet<CategoriaEquipo> CategoriaEquipos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Costo> Costos { get; set; }
        public DbSet<Diagnostico> Diagnosticos { get; set; }
        public DbSet<DiagxSol> DiagxSoles { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<ProbDiagSol> ProbDiagSoles { get; set; }
        public DbSet<Problema> Problemas { get; set; }
        public DbSet<ProbxDiag> ProbxDiags { get; set; }
        public DbSet<RankingClientes> RankingClientess { get; set; }
        public DbSet<RankingDiag> RankingDiags { get; set; }
        public DbSet<RankingSol> RankingSoles { get; set; }
        public DbSet<RankingUsuario> RankingUsuarios { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Solucion> Soluciones { get; set; }
        public DbSet<ServxUsuario> ServxUsuarios { get; set; }
    }
}
