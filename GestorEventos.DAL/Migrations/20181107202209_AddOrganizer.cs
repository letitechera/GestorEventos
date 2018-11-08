using Microsoft.EntityFrameworkCore.Migrations;

namespace GestorEventos.DAL.Migrations
{
    public partial class AddOrganizer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_EventSchedule_EventScheduleId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_Participant_ParticipantId",
                table: "Certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_EventTopic_EventTopicId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Location_LocationId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_AspNetUsers_OrganizerId1",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSchedule_Event_EventId",
                table: "EventSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Participant_Attendants_AttendantId",
                table: "Participant");

            migrationBuilder.DropForeignKey(
                name: "FK_Participant_Event_EventId",
                table: "Participant");

            migrationBuilder.DropForeignKey(
                name: "FK_Speaker_Activities_ActivityId",
                table: "Speaker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Speaker",
                table: "Speaker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Participant",
                table: "Participant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventTopic",
                table: "EventTopic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventSchedule",
                table: "EventSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Event",
                table: "Event");

            migrationBuilder.RenameTable(
                name: "Speaker",
                newName: "Speakers");

            migrationBuilder.RenameTable(
                name: "Participant",
                newName: "Participants");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");

            migrationBuilder.RenameTable(
                name: "EventTopic",
                newName: "EventTopics");

            migrationBuilder.RenameTable(
                name: "EventSchedule",
                newName: "EventSchedules");

            migrationBuilder.RenameTable(
                name: "Event",
                newName: "Events");

            migrationBuilder.RenameIndex(
                name: "IX_Speaker_ActivityId",
                table: "Speakers",
                newName: "IX_Speakers_ActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_Participant_EventId",
                table: "Participants",
                newName: "IX_Participants_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Participant_AttendantId",
                table: "Participants",
                newName: "IX_Participants_AttendantId");

            migrationBuilder.RenameIndex(
                name: "IX_EventSchedule_EventId",
                table: "EventSchedules",
                newName: "IX_EventSchedules_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_OrganizerId1",
                table: "Events",
                newName: "IX_Events_OrganizerId1");

            migrationBuilder.RenameIndex(
                name: "IX_Event_LocationId",
                table: "Events",
                newName: "IX_Events_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_EventTopicId",
                table: "Events",
                newName: "IX_Events_EventTopicId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Speakers",
                table: "Speakers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Participants",
                table: "Participants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventTopics",
                table: "EventTopics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventSchedules",
                table: "EventSchedules",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_EventSchedules_EventScheduleId",
                table: "Activities",
                column: "EventScheduleId",
                principalTable: "EventSchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_Participants_ParticipantId",
                table: "Certificates",
                column: "ParticipantId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventTopics_EventTopicId",
                table: "Events",
                column: "EventTopicId",
                principalTable: "EventTopics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Locations_LocationId",
                table: "Events",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_OrganizerId1",
                table: "Events",
                column: "OrganizerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSchedules_Events_EventId",
                table: "EventSchedules",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Attendants_AttendantId",
                table: "Participants",
                column: "AttendantId",
                principalTable: "Attendants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Events_EventId",
                table: "Participants",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Speakers_Activities_ActivityId",
                table: "Speakers",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_EventSchedules_EventScheduleId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_Participants_ParticipantId",
                table: "Certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventTopics_EventTopicId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Locations_LocationId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_OrganizerId1",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSchedules_Events_EventId",
                table: "EventSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Attendants_AttendantId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Events_EventId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Speakers_Activities_ActivityId",
                table: "Speakers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Speakers",
                table: "Speakers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Participants",
                table: "Participants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventTopics",
                table: "EventTopics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventSchedules",
                table: "EventSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.RenameTable(
                name: "Speakers",
                newName: "Speaker");

            migrationBuilder.RenameTable(
                name: "Participants",
                newName: "Participant");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            migrationBuilder.RenameTable(
                name: "EventTopics",
                newName: "EventTopic");

            migrationBuilder.RenameTable(
                name: "EventSchedules",
                newName: "EventSchedule");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "Event");

            migrationBuilder.RenameIndex(
                name: "IX_Speakers_ActivityId",
                table: "Speaker",
                newName: "IX_Speaker_ActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_Participants_EventId",
                table: "Participant",
                newName: "IX_Participant_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Participants_AttendantId",
                table: "Participant",
                newName: "IX_Participant_AttendantId");

            migrationBuilder.RenameIndex(
                name: "IX_EventSchedules_EventId",
                table: "EventSchedule",
                newName: "IX_EventSchedule_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_OrganizerId1",
                table: "Event",
                newName: "IX_Event_OrganizerId1");

            migrationBuilder.RenameIndex(
                name: "IX_Events_LocationId",
                table: "Event",
                newName: "IX_Event_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_EventTopicId",
                table: "Event",
                newName: "IX_Event_EventTopicId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Speaker",
                table: "Speaker",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Participant",
                table: "Participant",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventTopic",
                table: "EventTopic",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventSchedule",
                table: "EventSchedule",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Event",
                table: "Event",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_EventSchedule_EventScheduleId",
                table: "Activities",
                column: "EventScheduleId",
                principalTable: "EventSchedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_Participant_ParticipantId",
                table: "Certificates",
                column: "ParticipantId",
                principalTable: "Participant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_EventTopic_EventTopicId",
                table: "Event",
                column: "EventTopicId",
                principalTable: "EventTopic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Location_LocationId",
                table: "Event",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_AspNetUsers_OrganizerId1",
                table: "Event",
                column: "OrganizerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSchedule_Event_EventId",
                table: "EventSchedule",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participant_Attendants_AttendantId",
                table: "Participant",
                column: "AttendantId",
                principalTable: "Attendants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participant_Event_EventId",
                table: "Participant",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Speaker_Activities_ActivityId",
                table: "Speaker",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
