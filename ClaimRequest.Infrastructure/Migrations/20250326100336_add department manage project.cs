using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adddepartmentmanageproject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                schema: "public",
                table: "Projects",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DepartmentId",
                schema: "public",
                table: "Projects",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Departments_DepartmentId",
                schema: "public",
                table: "Projects",
                column: "DepartmentId",
                principalSchema: "public",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Departments_DepartmentId",
                schema: "public",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_DepartmentId",
                schema: "public",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                schema: "public",
                table: "Projects");
        }
    }
}
