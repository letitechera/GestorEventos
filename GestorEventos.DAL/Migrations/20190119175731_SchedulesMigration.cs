using Microsoft.EntityFrameworkCore.Migrations;

namespace GestorEventos.DAL.Migrations
{
    public partial class SchedulesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactUrl",
                table: "Speaker");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Speaker",
                newName: "Contact");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Contact",
                table: "Speaker",
                newName: "Email");

            migrationBuilder.AddColumn<string>(
                name: "ContactUrl",
                table: "Speaker",
                nullable: true);
        }
    }
}
