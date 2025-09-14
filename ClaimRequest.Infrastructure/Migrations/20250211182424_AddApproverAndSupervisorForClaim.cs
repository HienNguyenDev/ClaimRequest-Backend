using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddApproverAndSupervisorForClaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApproverId",
                schema: "public",
                table: "Claims",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ApproverId1",
                schema: "public",
                table: "Claims",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SuperivisorId",
                schema: "public",
                table: "Claims",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SuperivisorId1",
                schema: "public",
                table: "Claims",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Claims_ApproverId",
                schema: "public",
                table: "Claims",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_ApproverId1",
                schema: "public",
                table: "Claims",
                column: "ApproverId1");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_SuperivisorId",
                schema: "public",
                table: "Claims",
                column: "SuperivisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_SuperivisorId1",
                schema: "public",
                table: "Claims",
                column: "SuperivisorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Users_ApproverId",
                schema: "public",
                table: "Claims",
                column: "ApproverId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Users_ApproverId1",
                schema: "public",
                table: "Claims",
                column: "ApproverId1",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Users_SuperivisorId",
                schema: "public",
                table: "Claims",
                column: "SuperivisorId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Users_SuperivisorId1",
                schema: "public",
                table: "Claims",
                column: "SuperivisorId1",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Users_ApproverId",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Users_ApproverId1",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Users_SuperivisorId",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Users_SuperivisorId1",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropIndex(
                name: "IX_Claims_ApproverId",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropIndex(
                name: "IX_Claims_ApproverId1",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropIndex(
                name: "IX_Claims_SuperivisorId",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropIndex(
                name: "IX_Claims_SuperivisorId1",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ApproverId",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ApproverId1",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "SuperivisorId",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "SuperivisorId1",
                schema: "public",
                table: "Claims");
        }
    }
}
