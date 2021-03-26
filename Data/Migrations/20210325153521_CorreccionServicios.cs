using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceMVC.Data.Migrations
{
    public partial class CorreccionServicios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Clientes_ClienteID",
                table: "Servicios");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClienteID",
                table: "Servicios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Clientes_ClienteID",
                table: "Servicios",
                column: "ClienteID",
                principalTable: "Clientes",
                principalColumn: "ClienteID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Clientes_ClienteID",
                table: "Servicios");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClienteID",
                table: "Servicios",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Clientes_ClienteID",
                table: "Servicios",
                column: "ClienteID",
                principalTable: "Clientes",
                principalColumn: "ClienteID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
