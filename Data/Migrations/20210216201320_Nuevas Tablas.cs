using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceMVC.Data.Migrations
{
    public partial class NuevasTablas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "ArancelTiempos",
                columns: table => new
                {
                    ArancelTiempoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArancelTiempos", x => x.ArancelTiempoID);
                });

            migrationBuilder.CreateTable(
                name: "ArancelUnidades",
                columns: table => new
                {
                    ArancelUnidadID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArancelUnidades", x => x.ArancelUnidadID);
                });

            migrationBuilder.CreateTable(
                name: "CategoriaEquipos",
                columns: table => new
                {
                    CategoriaEquipoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaEquipos", x => x.CategoriaEquipoID);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Domicilio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelCel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelFijo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteID);
                });

            migrationBuilder.CreateTable(
                name: "Diagnosticos",
                columns: table => new
                {
                    DiagnosticoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnosticos", x => x.DiagnosticoID);
                });

            migrationBuilder.CreateTable(
                name: "Problemas",
                columns: table => new
                {
                    ProblemaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Problemas", x => x.ProblemaID);
                });

            migrationBuilder.CreateTable(
                name: "rankingUsuarios",
                columns: table => new
                {
                    RankingUsuarioID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnProceso = table.Column<int>(type: "int", nullable: false),
                    Solucionados = table.Column<int>(type: "int", nullable: false),
                    Fallados = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rankingUsuarios", x => x.RankingUsuarioID);
                });

            migrationBuilder.CreateTable(
                name: "Soluciones",
                columns: table => new
                {
                    SolucionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Soluciones", x => x.SolucionID);
                });

            migrationBuilder.CreateTable(
                name: "Equipos",
                columns: table => new
                {
                    EquipoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriaEquipoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipos", x => x.EquipoID);
                    table.ForeignKey(
                        name: "FK_Equipos_CategoriaEquipos_CategoriaEquipoID",
                        column: x => x.CategoriaEquipoID,
                        principalTable: "CategoriaEquipos",
                        principalColumn: "CategoriaEquipoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equipos_Clientes_ClienteID",
                        column: x => x.ClienteID,
                        principalTable: "Clientes",
                        principalColumn: "ClienteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RankingClientess",
                columns: table => new
                {
                    RankingClientesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalServicios = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankingClientess", x => x.RankingClientesID);
                    table.ForeignKey(
                        name: "FK_RankingClientess_Clientes_ClienteID",
                        column: x => x.ClienteID,
                        principalTable: "Clientes",
                        principalColumn: "ClienteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProbxDiags",
                columns: table => new
                {
                    ProbxDiagID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProblemaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiagnosticoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProbxDiags", x => x.ProbxDiagID);
                    table.ForeignKey(
                        name: "FK_ProbxDiags_Diagnosticos_DiagnosticoID",
                        column: x => x.DiagnosticoID,
                        principalTable: "Diagnosticos",
                        principalColumn: "DiagnosticoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProbxDiags_Problemas_ProblemaID",
                        column: x => x.ProblemaID,
                        principalTable: "Problemas",
                        principalColumn: "ProblemaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Costos",
                columns: table => new
                {
                    CostoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SolucionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tiempo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unidades = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Costos", x => x.CostoID);
                    table.ForeignKey(
                        name: "FK_Costos_Soluciones_SolucionID",
                        column: x => x.SolucionID,
                        principalTable: "Soluciones",
                        principalColumn: "SolucionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "diagxSoles",
                columns: table => new
                {
                    DiagxSolID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiagnosticoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SolucionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diagxSoles", x => x.DiagxSolID);
                    table.ForeignKey(
                        name: "FK_diagxSoles_Diagnosticos_DiagnosticoID",
                        column: x => x.DiagnosticoID,
                        principalTable: "Diagnosticos",
                        principalColumn: "DiagnosticoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_diagxSoles_Soluciones_SolucionID",
                        column: x => x.SolucionID,
                        principalTable: "Soluciones",
                        principalColumn: "SolucionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    ServicioID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaIng = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEgr = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TiempoTrabajo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnidadesTrabajo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Solucionado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.ServicioID);
                    table.ForeignKey(
                        name: "FK_Servicios_Equipos_EquipoID",
                        column: x => x.EquipoID,
                        principalTable: "Equipos",
                        principalColumn: "EquipoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rankingDiags",
                columns: table => new
                {
                    RankingDiagID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProbxDiagID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rankingDiags", x => x.RankingDiagID);
                    table.ForeignKey(
                        name: "FK_rankingDiags_ProbxDiags_ProbxDiagID",
                        column: x => x.ProbxDiagID,
                        principalTable: "ProbxDiags",
                        principalColumn: "ProbxDiagID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rankingSoles",
                columns: table => new
                {
                    RankingSolID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiagxSolID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rankingSoles", x => x.RankingSolID);
                    table.ForeignKey(
                        name: "FK_rankingSoles_diagxSoles_DiagxSolID",
                        column: x => x.DiagxSolID,
                        principalTable: "diagxSoles",
                        principalColumn: "DiagxSolID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProbDiagSoles",
                columns: table => new
                {
                    ProbDiagSolID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicioID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProblemaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiagnosticoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SolucionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProbDiagSoles", x => x.ProbDiagSolID);
                    table.ForeignKey(
                        name: "FK_ProbDiagSoles_Diagnosticos_DiagnosticoID",
                        column: x => x.DiagnosticoID,
                        principalTable: "Diagnosticos",
                        principalColumn: "DiagnosticoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProbDiagSoles_Problemas_ProblemaID",
                        column: x => x.ProblemaID,
                        principalTable: "Problemas",
                        principalColumn: "ProblemaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProbDiagSoles_Servicios_ServicioID",
                        column: x => x.ServicioID,
                        principalTable: "Servicios",
                        principalColumn: "ServicioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProbDiagSoles_Soluciones_SolucionID",
                        column: x => x.SolucionID,
                        principalTable: "Soluciones",
                        principalColumn: "SolucionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Costos_SolucionID",
                table: "Costos",
                column: "SolucionID");

            migrationBuilder.CreateIndex(
                name: "IX_diagxSoles_DiagnosticoID",
                table: "diagxSoles",
                column: "DiagnosticoID");

            migrationBuilder.CreateIndex(
                name: "IX_diagxSoles_SolucionID",
                table: "diagxSoles",
                column: "SolucionID");

            migrationBuilder.CreateIndex(
                name: "IX_Equipos_CategoriaEquipoID",
                table: "Equipos",
                column: "CategoriaEquipoID");

            migrationBuilder.CreateIndex(
                name: "IX_Equipos_ClienteID",
                table: "Equipos",
                column: "ClienteID");

            migrationBuilder.CreateIndex(
                name: "IX_ProbDiagSoles_DiagnosticoID",
                table: "ProbDiagSoles",
                column: "DiagnosticoID");

            migrationBuilder.CreateIndex(
                name: "IX_ProbDiagSoles_ProblemaID",
                table: "ProbDiagSoles",
                column: "ProblemaID");

            migrationBuilder.CreateIndex(
                name: "IX_ProbDiagSoles_ServicioID",
                table: "ProbDiagSoles",
                column: "ServicioID");

            migrationBuilder.CreateIndex(
                name: "IX_ProbDiagSoles_SolucionID",
                table: "ProbDiagSoles",
                column: "SolucionID");

            migrationBuilder.CreateIndex(
                name: "IX_ProbxDiags_DiagnosticoID",
                table: "ProbxDiags",
                column: "DiagnosticoID");

            migrationBuilder.CreateIndex(
                name: "IX_ProbxDiags_ProblemaID",
                table: "ProbxDiags",
                column: "ProblemaID");

            migrationBuilder.CreateIndex(
                name: "IX_RankingClientess_ClienteID",
                table: "RankingClientess",
                column: "ClienteID");

            migrationBuilder.CreateIndex(
                name: "IX_rankingDiags_ProbxDiagID",
                table: "rankingDiags",
                column: "ProbxDiagID");

            migrationBuilder.CreateIndex(
                name: "IX_rankingSoles_DiagxSolID",
                table: "rankingSoles",
                column: "DiagxSolID");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_EquipoID",
                table: "Servicios",
                column: "EquipoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArancelTiempos");

            migrationBuilder.DropTable(
                name: "ArancelUnidades");

            migrationBuilder.DropTable(
                name: "Costos");

            migrationBuilder.DropTable(
                name: "ProbDiagSoles");

            migrationBuilder.DropTable(
                name: "RankingClientess");

            migrationBuilder.DropTable(
                name: "rankingDiags");

            migrationBuilder.DropTable(
                name: "rankingSoles");

            migrationBuilder.DropTable(
                name: "rankingUsuarios");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "ProbxDiags");

            migrationBuilder.DropTable(
                name: "diagxSoles");

            migrationBuilder.DropTable(
                name: "Equipos");

            migrationBuilder.DropTable(
                name: "Problemas");

            migrationBuilder.DropTable(
                name: "Diagnosticos");

            migrationBuilder.DropTable(
                name: "Soluciones");

            migrationBuilder.DropTable(
                name: "CategoriaEquipos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }
    }
}
