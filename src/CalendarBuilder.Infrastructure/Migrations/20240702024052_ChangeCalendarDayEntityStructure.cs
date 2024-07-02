using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalendarBuilder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCalendarDayEntityStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarDays_Sports_FullSessionSportId",
                table: "CalendarDays");

            migrationBuilder.DropIndex(
                name: "IX_CalendarDays_FullSessionSportId",
                table: "CalendarDays");

            migrationBuilder.DropColumn(
                name: "FullSessionSportId",
                table: "CalendarDays");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FullSessionSportId",
                table: "CalendarDays",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CalendarDays_FullSessionSportId",
                table: "CalendarDays",
                column: "FullSessionSportId");

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarDays_Sports_FullSessionSportId",
                table: "CalendarDays",
                column: "FullSessionSportId",
                principalTable: "Sports",
                principalColumn: "Id");
        }
    }
}
