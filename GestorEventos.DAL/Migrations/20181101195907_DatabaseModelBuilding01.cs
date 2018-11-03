using Microsoft.EntityFrameworkCore.Migrations;

namespace GestorEventos.DAL.Migrations
{
    public partial class DatabaseModelBuilding01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_Participant_ParticipantId",
                table: "Certificates");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_Participant_ParticipantId",
                table: "Certificates",
                column: "ParticipantId",
                principalTable: "Participant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_Participant_ParticipantId",
                table: "Certificates");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_Participant_ParticipantId",
                table: "Certificates",
                column: "ParticipantId",
                principalTable: "Participant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
