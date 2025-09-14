using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserRoleEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "public",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("123e4567-e89b-12d3-a456-426614174000"),
                column: "Role",
                value: (byte)1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "public",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("123e4567-e89b-12d3-a456-426614174000"),
                column: "Role",
                value: (byte)4);
        }
    }
}
