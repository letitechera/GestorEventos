using Microsoft.EntityFrameworkCore.Migrations;

namespace GestorEventos.DAL.Migrations
{
    public partial class ParticipantCertificate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParticipantId",
                table: "Certificates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_ParticipantId",
                table: "Certificates",
                column: "ParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_Participant_ParticipantId",
                table: "Certificates",
                column: "ParticipantId",
                principalTable: "Participant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_Participant_ParticipantId",
                table: "Certificates");

            migrationBuilder.DropIndex(
                name: "IX_Certificates_ParticipantId",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "ParticipantId",
                table: "Certificates");
        }
    }
}
