using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixabnormalcaselateealrycase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "public",
                table: "LateEarlyCases",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "public",
                table: "AbnormalCases",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_LateEarlyCases_UserId",
                schema: "public",
                table: "LateEarlyCases",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AbnormalCases_UserId",
                schema: "public",
                table: "AbnormalCases",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbnormalCases_Users_UserId",
                schema: "public",
                table: "AbnormalCases",
                column: "UserId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LateEarlyCases_Users_UserId",
                schema: "public",
                table: "LateEarlyCases",
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
                name: "FK_AbnormalCases_Users_UserId",
                schema: "public",
                table: "AbnormalCases");

            migrationBuilder.DropForeignKey(
                name: "FK_LateEarlyCases_Users_UserId",
                schema: "public",
                table: "LateEarlyCases");

            migrationBuilder.DropIndex(
                name: "IX_LateEarlyCases_UserId",
                schema: "public",
                table: "LateEarlyCases");

            migrationBuilder.DropIndex(
                name: "IX_AbnormalCases_UserId",
                schema: "public",
                table: "AbnormalCases");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "public",
                table: "LateEarlyCases");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "public",
                table: "AbnormalCases");
        }
    }
}
