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

        public virtual DbSet<ArancelTiempo> ArancelTiempos  { get; set; }
        public virtual DbSet<ArancelUnidad> ArancelUnidades { get; set; }
        public virtual DbSet<CategoriaEquipo> CategoriaEquipos { get; set; }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Costo> Costos { get; set; }
        public virtual DbSet<Diagnostico> Diagnosticos { get; set; }
        public virtual DbSet<DiagxSol> diagxSoles { get; set; }
        public virtual DbSet<Equipo> Equipos { get; set; }
        public virtual DbSet<ProbDiagSol> ProbDiagSoles { get; set; }
        public virtual DbSet<Problema> Problemas { get; set; }
        public virtual DbSet<ProbxDiag> ProbxDiags { get; set; }
        public virtual DbSet<RankingClientes> RankingClientess { get; set; }
        public virtual DbSet<RankingDiag> rankingDiags { get; set; }
        public virtual DbSet<RankingSol> rankingSoles { get; set; }
        public virtual DbSet<RankingUsuario> rankingUsuarios { get; set; }
        public virtual DbSet<Servicio> Servicios { get; set; }
        public virtual DbSet<Solucion> Soluciones { get; set; }
    }
}
