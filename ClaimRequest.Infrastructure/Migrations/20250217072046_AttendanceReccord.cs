using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AttendanceReccord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLateCome",
                schema: "public",
                table: "AttendanceRecord",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLeaveEarly",
                schema: "public",
                table: "AttendanceRecord",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLateCome",
                schema: "public",
                table: "AttendanceRecord");

            migrationBuilder.DropColumn(
                name: "IsLeaveEarly",
                schema: "public",
                table: "AttendanceRecord");
        }
    }
}
