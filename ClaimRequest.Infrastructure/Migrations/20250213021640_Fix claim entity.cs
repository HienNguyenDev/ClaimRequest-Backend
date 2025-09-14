using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fixclaimentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClaimDetails_Claims_ClaimID",
                schema: "public",
                table: "ClaimDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Projects_ProjectId",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Users_SuperivisorId",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ClaimFee",
                schema: "public",
                table: "ClaimDetails");

            migrationBuilder.DropColumn(
                name: "EndTime",
                schema: "public",
                table: "ClaimDetails");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "public",
                table: "ClaimDetails");

            migrationBuilder.DropColumn(
                name: "StartTime",
                schema: "public",
                table: "ClaimDetails");

            migrationBuilder.DropColumn(
                name: "TotalHours",
                schema: "public",
                table: "ClaimDetails");

            migrationBuilder.RenameColumn(
                name: "SuperivisorId",
                schema: "public",
                table: "Claims",
                newName: "SupervisorId");

            migrationBuilder.RenameIndex(
                name: "IX_Claims_SuperivisorId",
                schema: "public",
                table: "Claims",
                newName: "IX_Claims_SupervisorId");

            migrationBuilder.RenameColumn(
                name: "ClaimID",
                schema: "public",
                table: "ClaimDetails",
                newName: "ClaimId");

            migrationBuilder.RenameIndex(
                name: "IX_ClaimDetails_ClaimID",
                schema: "public",
                table: "ClaimDetails",
                newName: "IX_ClaimDetails_ClaimId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                schema: "public",
                table: "Claims",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<decimal>(
                name: "ClaimFee",
                schema: "public",
                table: "Claims",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                schema: "public",
                table: "Claims",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<byte>(
                name: "Partial",
                schema: "public",
                table: "Claims",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                schema: "public",
                table: "Claims",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                schema: "public",
                table: "ClaimDetails",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimDetails_Claims_ClaimId",
                schema: "public",
                table: "ClaimDetails",
                column: "ClaimId",
                principalSchema: "public",
                principalTable: "Claims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Projects_ProjectId",
                schema: "public",
                table: "Claims",
                column: "ProjectId",
                principalSchema: "public",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Users_SupervisorId",
                schema: "public",
                table: "Claims",
                column: "SupervisorId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClaimDetails_Claims_ClaimId",
                schema: "public",
                table: "ClaimDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Projects_ProjectId",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Users_SupervisorId",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ClaimFee",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "EndDate",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "Partial",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "StartDate",
                schema: "public",
                table: "Claims");

            migrationBuilder.RenameColumn(
                name: "SupervisorId",
                schema: "public",
                table: "Claims",
                newName: "SuperivisorId");

            migrationBuilder.RenameIndex(
                name: "IX_Claims_SupervisorId",
                schema: "public",
                table: "Claims",
                newName: "IX_Claims_SuperivisorId");

            migrationBuilder.RenameColumn(
                name: "ClaimId",
                schema: "public",
                table: "ClaimDetails",
                newName: "ClaimID");

            migrationBuilder.RenameIndex(
                name: "IX_ClaimDetails_ClaimId",
                schema: "public",
                table: "ClaimDetails",
                newName: "IX_ClaimDetails_ClaimID");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                schema: "public",
                table: "Claims",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                schema: "public",
                table: "ClaimDetails",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<decimal>(
                name: "ClaimFee",
                schema: "public",
                table: "ClaimDetails",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                schema: "public",
                table: "ClaimDetails",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "public",
                table: "ClaimDetails",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                schema: "public",
                table: "ClaimDetails",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "TotalHours",
                schema: "public",
                table: "ClaimDetails",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimDetails_Claims_ClaimID",
                schema: "public",
                table: "ClaimDetails",
                column: "ClaimID",
                principalSchema: "public",
                principalTable: "Claims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Projects_ProjectId",
                schema: "public",
                table: "Claims",
                column: "ProjectId",
                principalSchema: "public",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Users_SuperivisorId",
                schema: "public",
                table: "Claims",
                column: "SuperivisorId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
