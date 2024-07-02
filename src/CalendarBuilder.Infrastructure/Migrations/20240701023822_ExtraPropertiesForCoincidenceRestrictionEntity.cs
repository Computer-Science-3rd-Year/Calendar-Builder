using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalendarBuilder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExtraPropertiesForCoincidenceRestrictionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CalendarId",
                table: "CoincidenceRestrictions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CoincidenceRestrictions_CalendarId",
                table: "CoincidenceRestrictions",
                column: "CalendarId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoincidenceRestrictions_Calendars_CalendarId",
                table: "CoincidenceRestrictions",
                column: "CalendarId",
                principalTable: "Calendars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoincidenceRestrictions_Calendars_CalendarId",
                table: "CoincidenceRestrictions");

            migrationBuilder.DropIndex(
                name: "IX_CoincidenceRestrictions_CalendarId",
                table: "CoincidenceRestrictions");

            migrationBuilder.DropColumn(
                name: "CalendarId",
                table: "CoincidenceRestrictions");
        }
    }
}
