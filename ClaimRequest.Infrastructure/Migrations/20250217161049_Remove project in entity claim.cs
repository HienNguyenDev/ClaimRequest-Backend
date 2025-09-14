using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Removeprojectinentityclaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Projects_ProjectId",
                schema: "public",
                table: "Claims");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                schema: "public",
                table: "Claims",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Projects_ProjectId",
                schema: "public",
                table: "Claims",
                column: "ProjectId",
                principalSchema: "public",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Projects_ProjectId",
                schema: "public",
                table: "Claims");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Projects_ProjectId",
                schema: "public",
                table: "Claims",
                column: "ProjectId",
                principalSchema: "public",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
