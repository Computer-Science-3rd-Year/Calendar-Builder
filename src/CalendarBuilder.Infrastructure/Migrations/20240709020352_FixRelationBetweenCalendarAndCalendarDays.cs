﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalendarBuilder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationBetweenCalendarAndCalendarDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarDays_Calendars_CalendarId",
                table: "CalendarDays");

            migrationBuilder.AlterColumn<Guid>(
                name: "CalendarId",
                table: "CalendarDays",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarDays_Calendars_CalendarId",
                table: "CalendarDays",
                column: "CalendarId",
                principalTable: "Calendars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarDays_Calendars_CalendarId",
                table: "CalendarDays");

            migrationBuilder.AlterColumn<Guid>(
                name: "CalendarId",
                table: "CalendarDays",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarDays_Calendars_CalendarId",
                table: "CalendarDays",
                column: "CalendarId",
                principalTable: "Calendars",
                principalColumn: "Id");
        }
    }
}
