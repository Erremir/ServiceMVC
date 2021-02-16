﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServiceMVC.Data;

namespace ServiceMVC.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ServiceMVC.Models.ArancelTiempo", b =>
                {
                    b.Property<Guid>("ArancelTiempoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ArancelTiempoID");

                    b.ToTable("ArancelTiempos");
                });

            modelBuilder.Entity("ServiceMVC.Models.ArancelUnidad", b =>
                {
                    b.Property<Guid>("ArancelUnidadID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ArancelUnidadID");

                    b.ToTable("ArancelUnidades");
                });

            modelBuilder.Entity("ServiceMVC.Models.CategoriaEquipo", b =>
                {
                    b.Property<Guid>("CategoriaEquipoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Categoria")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoriaEquipoID");

                    b.ToTable("CategoriaEquipos");
                });

            modelBuilder.Entity("ServiceMVC.Models.Cliente", b =>
                {
                    b.Property<Guid>("ClienteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Apellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Comentario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Domicilio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("TelCel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelFijo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClienteID");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("ServiceMVC.Models.Costo", b =>
                {
                    b.Property<Guid>("CostoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SolucionID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Tiempo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Unidades")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("CostoID");

                    b.HasIndex("SolucionID");

                    b.ToTable("Costos");
                });

            modelBuilder.Entity("ServiceMVC.Models.Diagnostico", b =>
                {
                    b.Property<Guid>("DiagnosticoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DiagnosticoID");

                    b.ToTable("Diagnosticos");
                });

            modelBuilder.Entity("ServiceMVC.Models.DiagxSol", b =>
                {
                    b.Property<Guid>("DiagxSolID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DiagnosticoID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SolucionID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DiagxSolID");

                    b.HasIndex("DiagnosticoID");

                    b.HasIndex("SolucionID");

                    b.ToTable("diagxSoles");
                });

            modelBuilder.Entity("ServiceMVC.Models.Equipo", b =>
                {
                    b.Property<Guid>("EquipoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoriaEquipoID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClienteID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EquipoID");

                    b.HasIndex("CategoriaEquipoID");

                    b.HasIndex("ClienteID");

                    b.ToTable("Equipos");
                });

            modelBuilder.Entity("ServiceMVC.Models.ProbDiagSol", b =>
                {
                    b.Property<Guid>("ProbDiagSolID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DiagnosticoID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProblemaID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ServicioID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SolucionID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProbDiagSolID");

                    b.HasIndex("DiagnosticoID");

                    b.HasIndex("ProblemaID");

                    b.HasIndex("ServicioID");

                    b.HasIndex("SolucionID");

                    b.ToTable("ProbDiagSoles");
                });

            modelBuilder.Entity("ServiceMVC.Models.Problema", b =>
                {
                    b.Property<Guid>("ProblemaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProblemaID");

                    b.ToTable("Problemas");
                });

            modelBuilder.Entity("ServiceMVC.Models.ProbxDiag", b =>
                {
                    b.Property<Guid>("ProbxDiagID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DiagnosticoID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProblemaID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProbxDiagID");

                    b.HasIndex("DiagnosticoID");

                    b.HasIndex("ProblemaID");

                    b.ToTable("ProbxDiags");
                });

            modelBuilder.Entity("ServiceMVC.Models.RankingClientes", b =>
                {
                    b.Property<Guid>("RankingClientesID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClienteID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TotalServicios")
                        .HasColumnType("int");

                    b.HasKey("RankingClientesID");

                    b.HasIndex("ClienteID");

                    b.ToTable("RankingClientess");
                });

            modelBuilder.Entity("ServiceMVC.Models.RankingDiag", b =>
                {
                    b.Property<Guid>("RankingDiagID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProbxDiagID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Total")
                        .HasColumnType("int");

                    b.HasKey("RankingDiagID");

                    b.HasIndex("ProbxDiagID");

                    b.ToTable("rankingDiags");
                });

            modelBuilder.Entity("ServiceMVC.Models.RankingSol", b =>
                {
                    b.Property<Guid>("RankingSolID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DiagxSolID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Total")
                        .HasColumnType("int");

                    b.HasKey("RankingSolID");

                    b.HasIndex("DiagxSolID");

                    b.ToTable("rankingSoles");
                });

            modelBuilder.Entity("ServiceMVC.Models.RankingUsuario", b =>
                {
                    b.Property<Guid>("RankingUsuarioID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("EnProceso")
                        .HasColumnType("int");

                    b.Property<int>("Fallados")
                        .HasColumnType("int");

                    b.Property<int>("Solucionados")
                        .HasColumnType("int");

                    b.Property<Guid>("UsuarioID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RankingUsuarioID");

                    b.ToTable("rankingUsuarios");
                });

            modelBuilder.Entity("ServiceMVC.Models.Servicio", b =>
                {
                    b.Property<Guid>("ServicioID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EquipoID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("FechaEgr")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaIng")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Solucionado")
                        .HasColumnType("bit");

                    b.Property<decimal>("TiempoTrabajo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("UnidadesTrabajo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("UsuarioID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ServicioID");

                    b.HasIndex("EquipoID");

                    b.ToTable("Servicios");
                });

            modelBuilder.Entity("ServiceMVC.Models.Solucion", b =>
                {
                    b.Property<Guid>("SolucionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SolucionID");

                    b.ToTable("Soluciones");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ServiceMVC.Models.Costo", b =>
                {
                    b.HasOne("ServiceMVC.Models.Solucion", "Solucion")
                        .WithMany()
                        .HasForeignKey("SolucionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Solucion");
                });

            modelBuilder.Entity("ServiceMVC.Models.DiagxSol", b =>
                {
                    b.HasOne("ServiceMVC.Models.Diagnostico", "Diagnostico")
                        .WithMany()
                        .HasForeignKey("DiagnosticoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceMVC.Models.Solucion", "Solucion")
                        .WithMany()
                        .HasForeignKey("SolucionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Diagnostico");

                    b.Navigation("Solucion");
                });

            modelBuilder.Entity("ServiceMVC.Models.Equipo", b =>
                {
                    b.HasOne("ServiceMVC.Models.CategoriaEquipo", "CategoriaEquipo")
                        .WithMany()
                        .HasForeignKey("CategoriaEquipoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceMVC.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoriaEquipo");

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("ServiceMVC.Models.ProbDiagSol", b =>
                {
                    b.HasOne("ServiceMVC.Models.Diagnostico", "Diagnostico")
                        .WithMany()
                        .HasForeignKey("DiagnosticoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceMVC.Models.Problema", "Problema")
                        .WithMany()
                        .HasForeignKey("ProblemaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceMVC.Models.Servicio", "Servicio")
                        .WithMany()
                        .HasForeignKey("ServicioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceMVC.Models.Solucion", "Solucion")
                        .WithMany()
                        .HasForeignKey("SolucionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Diagnostico");

                    b.Navigation("Problema");

                    b.Navigation("Servicio");

                    b.Navigation("Solucion");
                });

            modelBuilder.Entity("ServiceMVC.Models.ProbxDiag", b =>
                {
                    b.HasOne("ServiceMVC.Models.Diagnostico", "Diagnostico")
                        .WithMany()
                        .HasForeignKey("DiagnosticoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceMVC.Models.Problema", "Problema")
                        .WithMany()
                        .HasForeignKey("ProblemaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Diagnostico");

                    b.Navigation("Problema");
                });

            modelBuilder.Entity("ServiceMVC.Models.RankingClientes", b =>
                {
                    b.HasOne("ServiceMVC.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("ServiceMVC.Models.RankingDiag", b =>
                {
                    b.HasOne("ServiceMVC.Models.ProbxDiag", "ProbxDiag")
                        .WithMany()
                        .HasForeignKey("ProbxDiagID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProbxDiag");
                });

            modelBuilder.Entity("ServiceMVC.Models.RankingSol", b =>
                {
                    b.HasOne("ServiceMVC.Models.DiagxSol", "DiagxSol")
                        .WithMany()
                        .HasForeignKey("DiagxSolID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DiagxSol");
                });

            modelBuilder.Entity("ServiceMVC.Models.Servicio", b =>
                {
                    b.HasOne("ServiceMVC.Models.Equipo", "Equipo")
                        .WithMany()
                        .HasForeignKey("EquipoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipo");
                });
#pragma warning restore 612, 618
        }
    }
}
