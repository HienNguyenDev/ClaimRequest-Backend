using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class create_decription_in_department : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "public",
                table: "Departments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "public",
                table: "Departments");
        }
    }
}
