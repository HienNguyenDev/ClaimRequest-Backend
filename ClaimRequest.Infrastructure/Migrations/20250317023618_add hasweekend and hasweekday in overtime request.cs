using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addhasweekendandhasweekdayinovertimerequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasWeekday",
                schema: "public",
                table: "OverTimeRequests",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasWeekend",
                schema: "public",
                table: "OverTimeRequests",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasWeekday",
                schema: "public",
                table: "OverTimeRequests");

            migrationBuilder.DropColumn(
                name: "HasWeekend",
                schema: "public",
                table: "OverTimeRequests");
        }
    }
}
