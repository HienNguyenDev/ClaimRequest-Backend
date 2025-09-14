using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixclaimroleinproject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RoleInProject",
                schema: "public",
                table: "Claims",
                type: "text",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "RoleInProject",
                schema: "public",
                table: "Claims",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
