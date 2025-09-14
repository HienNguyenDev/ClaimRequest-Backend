using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanySetting",
                schema: "public",
                table: "CompanySetting");

            migrationBuilder.RenameTable(
                name: "CompanySetting",
                schema: "public",
                newName: "CompanySettings",
                newSchema: "public");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "WorkStartTime",
                schema: "public",
                table: "CompanySettings",
                type: "time without time zone",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "interval");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "WorkEndTime",
                schema: "public",
                table: "CompanySettings",
                type: "time without time zone",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "interval");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanySettings",
                schema: "public",
                table: "CompanySettings",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanySettings",
                schema: "public",
                table: "CompanySettings");

            migrationBuilder.RenameTable(
                name: "CompanySettings",
                schema: "public",
                newName: "CompanySetting",
                newSchema: "public");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "WorkStartTime",
                schema: "public",
                table: "CompanySetting",
                type: "interval",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "WorkEndTime",
                schema: "public",
                table: "CompanySetting",
                type: "interval",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanySetting",
                schema: "public",
                table: "CompanySetting",
                column: "Id");
        }
    }
}
