using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalendarBuilder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCalendarEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CalendarId",
                table: "CalendarDays",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Calendars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendars", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalendarDays_CalendarId",
                table: "CalendarDays",
                column: "CalendarId");

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarDays_Calendars_CalendarId",
                table: "CalendarDays",
                column: "CalendarId",
                principalTable: "Calendars",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarDays_Calendars_CalendarId",
                table: "CalendarDays");

            migrationBuilder.DropTable(
                name: "Calendars");

            migrationBuilder.DropIndex(
                name: "IX_CalendarDays_CalendarId",
                table: "CalendarDays");

            migrationBuilder.DropColumn(
                name: "CalendarId",
                table: "CalendarDays");
        }
    }
}
