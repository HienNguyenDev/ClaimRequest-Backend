using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class migrateclaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Projects_ProjectId",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "RoleInProject",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "StaffDepartment",
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
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "RoleInProject",
                schema: "public",
                table: "Claims",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StaffDepartment",
                schema: "public",
                table: "Claims",
                type: "varchar",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Projects_ProjectId",
                schema: "public",
                table: "Claims",
                column: "ProjectId",
                principalSchema: "public",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
