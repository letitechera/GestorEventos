using Microsoft.EntityFrameworkCore.Migrations;

namespace GestorEventos.DAL.Migrations
{
    public partial class participant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CellPhone",
                table: "Participants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Participants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Participants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Participants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Participants",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CellPhone",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Participants");
        }
    }
}
