using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixlateearlycasewithclaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClaimDetails_LateEarlyId",
                schema: "public",
                table: "ClaimDetails");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimDetails_LateEarlyId",
                schema: "public",
                table: "ClaimDetails",
                column: "LateEarlyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClaimDetails_LateEarlyId",
                schema: "public",
                table: "ClaimDetails");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimDetails_LateEarlyId",
                schema: "public",
                table: "ClaimDetails",
                column: "LateEarlyId",
                unique: true);
        }
    }
}
