using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fixentityattendancerecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceRecord_Users_UserId",
                schema: "public",
                table: "AttendanceRecord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttendanceRecord",
                schema: "public",
                table: "AttendanceRecord");

            migrationBuilder.RenameTable(
                name: "AttendanceRecord",
                schema: "public",
                newName: "AttendanceRecords",
                newSchema: "public");

            migrationBuilder.RenameIndex(
                name: "IX_AttendanceRecord_UserId",
                schema: "public",
                table: "AttendanceRecords",
                newName: "IX_AttendanceRecords_UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "AttendanceId",
                schema: "public",
                table: "Claims",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "WorkStatus",
                schema: "public",
                table: "AttendanceRecords",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "smallint");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "WorkDate",
                schema: "public",
                table: "AttendanceRecords",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckInTime",
                schema: "public",
                table: "AttendanceRecords",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttendanceRecords",
                schema: "public",
                table: "AttendanceRecords",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_AttendanceId",
                schema: "public",
                table: "Claims",
                column: "AttendanceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceRecords_Users_UserId",
                schema: "public",
                table: "AttendanceRecords",
                column: "UserId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_AttendanceRecords_AttendanceId",
                schema: "public",
                table: "Claims",
                column: "AttendanceId",
                principalSchema: "public",
                principalTable: "AttendanceRecords",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceRecords_Users_UserId",
                schema: "public",
                table: "AttendanceRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Claims_AttendanceRecords_AttendanceId",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropIndex(
                name: "IX_Claims_AttendanceId",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttendanceRecords",
                schema: "public",
                table: "AttendanceRecords");

            migrationBuilder.DropColumn(
                name: "AttendanceId",
                schema: "public",
                table: "Claims");

            migrationBuilder.RenameTable(
                name: "AttendanceRecords",
                schema: "public",
                newName: "AttendanceRecord",
                newSchema: "public");

            migrationBuilder.RenameIndex(
                name: "IX_AttendanceRecords_UserId",
                schema: "public",
                table: "AttendanceRecord",
                newName: "IX_AttendanceRecord_UserId");

            migrationBuilder.AlterColumn<byte>(
                name: "WorkStatus",
                schema: "public",
                table: "AttendanceRecord",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(byte),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "WorkDate",
                schema: "public",
                table: "AttendanceRecord",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckInTime",
                schema: "public",
                table: "AttendanceRecord",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttendanceRecord",
                schema: "public",
                table: "AttendanceRecord",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceRecord_Users_UserId",
                schema: "public",
                table: "AttendanceRecord",
                column: "UserId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
