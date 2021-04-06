using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceMVC.Data.Migrations
{
    public partial class EstadoEnServicios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Finalizado",
                table: "Servicios",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finalizado",
                table: "Servicios");
        }
    }
}
