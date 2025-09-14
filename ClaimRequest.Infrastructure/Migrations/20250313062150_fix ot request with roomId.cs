using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixotrequestwithroomId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                schema: "public",
                table: "OverTimeRequests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_OverTimeRequests_RoomId",
                schema: "public",
                table: "OverTimeRequests",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_OverTimeRequests_Rooms_RoomId",
                schema: "public",
                table: "OverTimeRequests",
                column: "RoomId",
                principalSchema: "public",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OverTimeRequests_Rooms_RoomId",
                schema: "public",
                table: "OverTimeRequests");

            migrationBuilder.DropIndex(
                name: "IX_OverTimeRequests_RoomId",
                schema: "public",
                table: "OverTimeRequests");

            migrationBuilder.DropColumn(
                name: "RoomId",
                schema: "public",
                table: "OverTimeRequests");
        }
    }
}
