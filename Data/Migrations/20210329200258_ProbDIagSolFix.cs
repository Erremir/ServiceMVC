using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceMVC.Data.Migrations
{
    public partial class ProbDIagSolFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProbDiagSoles_Diagnosticos_DiagnosticoID",
                table: "ProbDiagSoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ProbDiagSoles_Problemas_ProblemaID",
                table: "ProbDiagSoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ProbDiagSoles_Soluciones_SolucionID",
                table: "ProbDiagSoles");

            migrationBuilder.AlterColumn<Guid>(
                name: "SolucionID",
                table: "ProbDiagSoles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProblemaID",
                table: "ProbDiagSoles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "DiagnosticoID",
                table: "ProbDiagSoles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_ProbDiagSoles_Diagnosticos_DiagnosticoID",
                table: "ProbDiagSoles",
                column: "DiagnosticoID",
                principalTable: "Diagnosticos",
                principalColumn: "DiagnosticoID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProbDiagSoles_Problemas_ProblemaID",
                table: "ProbDiagSoles",
                column: "ProblemaID",
                principalTable: "Problemas",
                principalColumn: "ProblemaID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProbDiagSoles_Soluciones_SolucionID",
                table: "ProbDiagSoles",
                column: "SolucionID",
                principalTable: "Soluciones",
                principalColumn: "SolucionID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProbDiagSoles_Diagnosticos_DiagnosticoID",
                table: "ProbDiagSoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ProbDiagSoles_Problemas_ProblemaID",
                table: "ProbDiagSoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ProbDiagSoles_Soluciones_SolucionID",
                table: "ProbDiagSoles");

            migrationBuilder.AlterColumn<Guid>(
                name: "SolucionID",
                table: "ProbDiagSoles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProblemaID",
                table: "ProbDiagSoles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DiagnosticoID",
                table: "ProbDiagSoles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProbDiagSoles_Diagnosticos_DiagnosticoID",
                table: "ProbDiagSoles",
                column: "DiagnosticoID",
                principalTable: "Diagnosticos",
                principalColumn: "DiagnosticoID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProbDiagSoles_Problemas_ProblemaID",
                table: "ProbDiagSoles",
                column: "ProblemaID",
                principalTable: "Problemas",
                principalColumn: "ProblemaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProbDiagSoles_Soluciones_SolucionID",
                table: "ProbDiagSoles",
                column: "SolucionID",
                principalTable: "Soluciones",
                principalColumn: "SolucionID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
