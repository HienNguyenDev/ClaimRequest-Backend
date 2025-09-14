using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixabnormalcaseentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClaimDetails_AbnormalId",
                schema: "public",
                table: "ClaimDetails");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimDetails_AbnormalId",
                schema: "public",
                table: "ClaimDetails",
                column: "AbnormalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClaimDetails_AbnormalId",
                schema: "public",
                table: "ClaimDetails");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimDetails_AbnormalId",
                schema: "public",
                table: "ClaimDetails",
                column: "AbnormalId",
                unique: true);
        }
    }
}
