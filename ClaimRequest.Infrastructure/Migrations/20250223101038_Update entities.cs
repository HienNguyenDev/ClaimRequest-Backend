using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updateentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_AttendanceRecords_AttendanceId",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropIndex(
                name: "IX_Claims_AttendanceId",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "AttendanceId",
                schema: "public",
                table: "Claims");

            migrationBuilder.AddColumn<Guid>(
                name: "AttendanceId",
                schema: "public",
                table: "ClaimDetails",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClaimDetails_AttendanceId",
                schema: "public",
                table: "ClaimDetails",
                column: "AttendanceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimDetails_AttendanceRecords_AttendanceId",
                schema: "public",
                table: "ClaimDetails",
                column: "AttendanceId",
                principalSchema: "public",
                principalTable: "AttendanceRecords",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClaimDetails_AttendanceRecords_AttendanceId",
                schema: "public",
                table: "ClaimDetails");

            migrationBuilder.DropIndex(
                name: "IX_ClaimDetails_AttendanceId",
                schema: "public",
                table: "ClaimDetails");

            migrationBuilder.DropColumn(
                name: "AttendanceId",
                schema: "public",
                table: "ClaimDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "AttendanceId",
                schema: "public",
                table: "Claims",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Claims_AttendanceId",
                schema: "public",
                table: "Claims",
                column: "AttendanceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_AttendanceRecords_AttendanceId",
                schema: "public",
                table: "Claims",
                column: "AttendanceId",
                principalSchema: "public",
                principalTable: "AttendanceRecords",
                principalColumn: "Id");
        }
    }
}
