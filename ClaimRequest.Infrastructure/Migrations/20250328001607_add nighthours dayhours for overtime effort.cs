using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addnighthoursdayhoursforovertimeeffort : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Hours",
                schema: "public",
                table: "OverTimeEffort",
                newName: "NightHours");

            migrationBuilder.AddColumn<int>(
                name: "DayHours",
                schema: "public",
                table: "OverTimeEffort",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayHours",
                schema: "public",
                table: "OverTimeEffort");

            migrationBuilder.RenameColumn(
                name: "NightHours",
                schema: "public",
                table: "OverTimeEffort",
                newName: "Hours");
        }
    }
}
