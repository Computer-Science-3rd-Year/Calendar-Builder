using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalendarBuilder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddActvieFieldToConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CoincidenceRestrictions",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CoincidenceRestrictions");
        }
    }
}
