using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixsalaryentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalaryPerMonth_Users_UserId1",
                schema: "public",
                table: "SalaryPerMonth");

            migrationBuilder.DropIndex(
                name: "IX_SalaryPerMonth_UserId1",
                schema: "public",
                table: "SalaryPerMonth");

            migrationBuilder.DropColumn(
                name: "UserId1",
                schema: "public",
                table: "SalaryPerMonth");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                schema: "public",
                table: "SalaryPerMonth",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalaryPerMonth_UserId1",
                schema: "public",
                table: "SalaryPerMonth",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryPerMonth_Users_UserId1",
                schema: "public",
                table: "SalaryPerMonth",
                column: "UserId1",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
