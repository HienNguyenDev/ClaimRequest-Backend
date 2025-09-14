using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AbnormalCaseLateEarlyCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClaimDetails_AttendanceRecords_AttendanceId",
                schema: "public",
                table: "ClaimDetails");

            migrationBuilder.DropColumn(
                name: "Remark",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "WorkStatus",
                schema: "public",
                table: "AttendanceRecords");

            migrationBuilder.RenameColumn(
                name: "AttendanceId",
                schema: "public",
                table: "ClaimDetails",
                newName: "LateEarlyId");

            migrationBuilder.RenameIndex(
                name: "IX_ClaimDetails_AttendanceId",
                schema: "public",
                table: "ClaimDetails",
                newName: "IX_ClaimDetails_LateEarlyId");

            migrationBuilder.AddColumn<Guid>(
                name: "AbnormalId",
                schema: "public",
                table: "ClaimDetails",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AbnormalCases",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbnormalCases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LateEarlyCases",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CheckInTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CheckoutTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsLateCome = table.Column<bool>(type: "boolean", nullable: false),
                    IsEarlyLeave = table.Column<bool>(type: "boolean", nullable: false),
                    WorkDate = table.Column<DateOnly>(type: "date", nullable: false),
                    LateTimeSpan = table.Column<TimeSpan>(type: "interval", nullable: false),
                    EarlyTimeSpan = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LateEarlyCases", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClaimDetails_AbnormalId",
                schema: "public",
                table: "ClaimDetails",
                column: "AbnormalId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimDetails_AbnormalCases_AbnormalId",
                schema: "public",
                table: "ClaimDetails",
                column: "AbnormalId",
                principalSchema: "public",
                principalTable: "AbnormalCases",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimDetails_LateEarlyCases_LateEarlyId",
                schema: "public",
                table: "ClaimDetails",
                column: "LateEarlyId",
                principalSchema: "public",
                principalTable: "LateEarlyCases",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClaimDetails_AbnormalCases_AbnormalId",
                schema: "public",
                table: "ClaimDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ClaimDetails_LateEarlyCases_LateEarlyId",
                schema: "public",
                table: "ClaimDetails");

            migrationBuilder.DropTable(
                name: "AbnormalCases",
                schema: "public");

            migrationBuilder.DropTable(
                name: "LateEarlyCases",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_ClaimDetails_AbnormalId",
                schema: "public",
                table: "ClaimDetails");

            migrationBuilder.DropColumn(
                name: "AbnormalId",
                schema: "public",
                table: "ClaimDetails");

            migrationBuilder.RenameColumn(
                name: "LateEarlyId",
                schema: "public",
                table: "ClaimDetails",
                newName: "AttendanceId");

            migrationBuilder.RenameIndex(
                name: "IX_ClaimDetails_LateEarlyId",
                schema: "public",
                table: "ClaimDetails",
                newName: "IX_ClaimDetails_AttendanceId");

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                schema: "public",
                table: "Claims",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "WorkStatus",
                schema: "public",
                table: "AttendanceRecords",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimDetails_AttendanceRecords_AttendanceId",
                schema: "public",
                table: "ClaimDetails",
                column: "AttendanceId",
                principalSchema: "public",
                principalTable: "AttendanceRecords",
                principalColumn: "Id");
        }
    }
}
