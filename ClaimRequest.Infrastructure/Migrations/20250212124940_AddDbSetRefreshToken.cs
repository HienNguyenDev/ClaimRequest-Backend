using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDbSetRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Users_UserId",
                schema: "public",
                table: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                schema: "public",
                table: "RefreshToken");

            migrationBuilder.RenameTable(
                name: "RefreshToken",
                schema: "public",
                newName: "RefreshTokens",
                newSchema: "public");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_UserId",
                schema: "public",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_Token",
                schema: "public",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_Token");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                schema: "public",
                table: "RefreshTokens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Users_UserId",
                schema: "public",
                table: "RefreshTokens",
                column: "UserId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Users_UserId",
                schema: "public",
                table: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                schema: "public",
                table: "RefreshTokens");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                schema: "public",
                newName: "RefreshToken",
                newSchema: "public");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_UserId",
                schema: "public",
                table: "RefreshToken",
                newName: "IX_RefreshToken_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_Token",
                schema: "public",
                table: "RefreshToken",
                newName: "IX_RefreshToken_Token");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                schema: "public",
                table: "RefreshToken",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Users_UserId",
                schema: "public",
                table: "RefreshToken",
                column: "UserId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
