using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalendarBuilder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCalendarDayEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalendarDays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MorningSessionSportId = table.Column<Guid>(type: "uuid", nullable: true),
                    AfterNoonSessionSportId = table.Column<Guid>(type: "uuid", nullable: true),
                    FullSessionSportId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalendarDays_Sports_AfterNoonSessionSportId",
                        column: x => x.AfterNoonSessionSportId,
                        principalTable: "Sports",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CalendarDays_Sports_FullSessionSportId",
                        column: x => x.FullSessionSportId,
                        principalTable: "Sports",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CalendarDays_Sports_MorningSessionSportId",
                        column: x => x.MorningSessionSportId,
                        principalTable: "Sports",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalendarDays_AfterNoonSessionSportId",
                table: "CalendarDays",
                column: "AfterNoonSessionSportId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarDays_FullSessionSportId",
                table: "CalendarDays",
                column: "FullSessionSportId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarDays_MorningSessionSportId",
                table: "CalendarDays",
                column: "MorningSessionSportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarDays");
        }
    }
}
