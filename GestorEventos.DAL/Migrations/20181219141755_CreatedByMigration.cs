using Microsoft.EntityFrameworkCore.Migrations;

namespace GestorEventos.DAL.Migrations
{
    public partial class CreatedByMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_AppUserId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_AppUserId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Events");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Events",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_AppUserId",
                table: "Events",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_AppUserId",
                table: "Events",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
