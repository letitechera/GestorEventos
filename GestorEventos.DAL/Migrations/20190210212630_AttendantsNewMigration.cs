using Microsoft.EntityFrameworkCore.Migrations;

namespace GestorEventos.DAL.Migrations
{
    public partial class AttendantsNewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CellPhone",
                table: "Attendants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Attendants",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CellPhone",
                table: "Attendants");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Attendants");
        }
    }
}
