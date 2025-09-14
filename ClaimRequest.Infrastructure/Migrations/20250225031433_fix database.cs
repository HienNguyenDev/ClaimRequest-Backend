using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Users_SupervisorId",
                schema: "public",
                table: "Claims");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Users_SupervisorId",
                schema: "public",
                table: "Claims",
                column: "SupervisorId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Users_SupervisorId",
                schema: "public",
                table: "Claims");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Users_SupervisorId",
                schema: "public",
                table: "Claims",
                column: "SupervisorId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
